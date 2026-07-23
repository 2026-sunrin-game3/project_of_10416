using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    PlayerMovement movement;
    playerAnimator animator;
    public int a = 1;
    public bool blocking = false;
    PlayerBattle battle;
    [SerializeField] RikaAnimator rika_ani;
    [SerializeField] EntityHealth health;
    

    public Vector2 axis;
    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        battle = GetComponent<PlayerBattle>();
        animator = GetComponent<playerAnimator>();
    }

    public void OnMove(InputValue value)
    {
        Vector2 axis_ = value.Get<Vector2>();
        axis = new Vector2(axis_.x, 0);
    }

    public bool HasAxis()
    {
        return axis.x != 0 || axis.y != 0;
    }
    public void OnJump()
    {
        if (movement.Jump())
            animator.Jump();
    }

    public void OnAttack()
    {
       
        if (a == 1)
        {
            if (battle.Attack())
            {
                animator.Play("Attack1");
                a = 2;
                if (battle.full_emergence)
                {
                    rika_ani.play("Attack");
                   

                }

            }
            
        }
        else if (a == 2)
        {
            if (battle.Attack())
            {
                animator.Play("Attack2");
                a = 1;
            }
            
        }
        
    }
    public void OnDash()
    {
        battle.Dash((int)animator.direction);
    }
    public void OnSkill1()
    {
        battle.Skill1();
    }
    void Update()
    {
        blocking = Keyboard.current.fKey.isPressed;
        animator.Guard();
    }
}
