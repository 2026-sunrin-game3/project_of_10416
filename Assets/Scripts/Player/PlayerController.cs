using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public PlayerInput input;
    public PlayerMovement movement;
    public playerAnimator animator;
    void Start()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<playerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.Move(input.axis);

        animator.SetMoving(input.HasAxis(), input.axis);
    }
}
