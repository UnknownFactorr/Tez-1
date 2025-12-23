using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveTopDown : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D playerRB;
    private Animator animator;

    public bool canWalk = false;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        canWalk = true;
    }

    void Update()
    {
        if (canWalk)
        {
            playerRB.linearVelocity = moveInput * moveSpeed;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!PauseMenu.GameIsPaused)
        {
            animator.SetBool("IsWalking", true);

            // ".canceled" is "when no button is pressed"
            // WARNING : "Last" inputs need to be set before "moveInput" so they actually get the last input by the player.
            if (context.canceled)
            {
                animator.SetBool("IsWalking", false);
                animator.SetFloat("LastInputX", moveInput.x);
                animator.SetFloat("LastInputY", moveInput.y);
            }

            moveInput = context.ReadValue<Vector2>();

            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
        }
    }
}

