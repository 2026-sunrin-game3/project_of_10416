using UnityEngine;

public class RikaAnimator : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void play(string id)
    {
        animator.Play(id);
    }
}
