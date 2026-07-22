using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator animator;
    EntityStat stat;
    public float direction;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        stat = GetComponent<EntityStat>();
    }
    public void SetMoving(bool val, Vector2 axis)
    {
        animator.SetBool("IsMoving", val);
        float Moverate = stat.GetResultValue("moveSpeed") / stat.GetBaseValue("moveSpeed");

        animator.SetFloat("MoveSpeed", Moverate);

        if (val)
        {
            if (axis.x > 0)
                direction = 1;
            else
                direction = -1;

            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y);
        }
    }

    public void Jump(float axis)
    {
        animator.SetTrigger("Jump");
        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y);

    }
    void Update()
    {
        
    }
}
