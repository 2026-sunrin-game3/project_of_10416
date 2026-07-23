using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class playerAnimator : MonoBehaviour
{
    Animator animator;

    EntityStat stat;
    PlayerInput guard;
    [SerializeField] RikaAnimator rika;
    [SerializeField] GameObject targetObject;
    [SerializeField] EntityHealth hp;
    
    public float direction;
    public float series_guard = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
        stat = GetComponent<EntityStat>();
        guard = GetComponent<PlayerInput>();
       
    }

   public void SetMoving(bool val, Vector2 axis)
    {
        if (!guard.blocking)
        {
            animator.SetBool("isMoving", val);

            float moveRate = stat.GetResultValue("moveSpeed") / stat.GetBaseValue("moveSpeed");

            animator.SetFloat("moveSpeed", moveRate);

            if(val)
            {
                if (axis.x > 0)
                    direction = 1;
                else if (axis.x < 0)
                    direction = -1;

                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y);
            }
        }
       
       
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void Play(string id)
    {
        animator.Play(id);
    }
    public void Guard()
    {
        if (guard.blocking)
        {
            animator.SetBool("Blocking", true);
            
            Debug.Log(series_guard);
            if (series_guard > 4)
            {

                
                rika.play("Attack2");
                series_guard = 0;
                hp.GetDamage(30);
            }


        }
        else { 
            animator.SetBool("Blocking", false);
            series_guard = 0;
        }
        
        

    }
}
