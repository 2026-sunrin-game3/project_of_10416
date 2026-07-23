using JetBrains.Annotations;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct AttackRange
{
    public Vector2 offset, size;
    public bool drawGizmos;
    
}
public class PlayerBattle : MonoBehaviour
{
    public EntityHealth health;
    public PlayerMovement movement;
    public EntityStat stat;
    public playerAnimator animator;
    public PlayerInput input;
    [SerializeField] Rika rika;
    [SerializeField] Ring ring;
    [SerializeField] Hide love;


    [SerializeField] DamageIndicator indicator;

    public float atkCool;

    public bool full_emergence = false;
    

    public AttackRange defaultAttack;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float dashpower, dashTime;
    public bool inDash;

    [SerializeField] Slider healthbar;
    void Start()
    {
        health = GetComponent<EntityHealth>();
        stat = GetComponent<EntityStat>();
        animator = GetComponent<playerAnimator>();
        movement = GetComponent<PlayerMovement>();
        input = GetComponent<PlayerInput>();
        

        health.OnDamage(OnHurt);
        rika.gameObject.SetActive(false);
    }

    void OnHurt(EntityHealth.Context ctx)
    {
        if (inDash)
            ctx.canceled = true;
        if (ctx.canceled)
            return;
        indicator.IndicateDamage(ctx.damage, transform.position + new Vector3(0, 1), Color.red);
    }
    void Update()
    {
        healthbar.value = health.health / health.maxHealth;
        if (atkCool > 0)
            atkCool -= Time.deltaTime * (1 + stat.GetResultValue("atkSpeed") / 100);
        defaultAttack.offset.x = animator.direction;
        if (health.isDeath) Destroy(gameObject);
        
        
    }

    public void Dash(int direction)
    {
         StartCoroutine(dash_(direction));
    }
    IEnumerator dash_(int direction) {
        inDash = true;
        movement.SetVelocity(Vector2.right * direction * dashpower);

        yield return new WaitForSeconds(dashTime);

        movement.SetVelocity(Vector2.zero);
        inDash = false;
    }
    public bool Attack()
    {
        if (atkCool > 0)
            return false;
        atkCool = 0.3f;
        var col = Physics2D.OverlapBoxAll((Vector2)transform.position + defaultAttack.offset, defaultAttack.size, 0, enemyMask);

        foreach (var target in col)
        {
            EntityHealth hp = target.GetComponent<EntityHealth>();
            if (hp != null)
            {
                hp.GetDamage(stat.GetResultValue("attackDamage"), health);
                if (full_emergence && input.a == 1)
                {
                    for (int i = 0; i< 3; i++)
                    {
                        hp.GetDamage(5, health);
                    }
                    
                    
                }
            }
        }
        return true;
    }
    public void Skill1()
    {
        if (full_emergence == false) { 
            StartCoroutine(skill1_());
        }
        else if(full_emergence == true)
        {
            StartCoroutine(purelove_());
        }
        
    }
    IEnumerator skill1_()
    {
        full_emergence = true;
        rika.gameObject.SetActive(true);
        rika.Full_Emergence();
        ring.Full_Emegence();
        love.Show();
        var atkBuf = new EntityStat.Buf
        {
            Key = "attackDamage",
            mathType = MathType.Increase,
            Value = 5
        };
        var atkspeedBuf = new EntityStat.Buf
        {
            Key = "atkSpeed",
            mathType = MathType.Add,
            Value = 50
        };
        stat.bufs.Add(atkBuf);
        stat.bufs.Add(atkspeedBuf);
        stat.Calc("attackDamage");
        stat.Calc("atkSpeed");

        yield return new WaitForSeconds(5);

       
            
    }
    IEnumerator purelove_()
    {
        yield return new WaitForSeconds(3);
    }
    void Draw(AttackRange range) 
    {
        if (!range.drawGizmos)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((Vector2)transform.position + range.offset, range.size);
    }

    void OnDrawGizmos() 
    {
        Draw(defaultAttack);
    }
    
    
    
}
