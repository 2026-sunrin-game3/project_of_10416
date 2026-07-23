using UnityEngine;
using UnityEngine.Animations;

public class Rika : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    [SerializeField] Transform player;
    Vector2 axis;
    Vector3 offset;
    float direction, distance = 2f;
    void Start()
    {
        
        
    }

    
    void Update()
    {
        axis = input.axis;
        if (axis.x > 0) direction = 1;
        else if (axis.x < 0)direction = -1;
        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y);
        offset = new Vector2(-direction * distance, 0.3f);
        transform.position = player.position + offset;
       
    }
    public void Full_Emergence()
    {
        
        transform.position = player.position + offset;
        

    }
    
}
