using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovePlatformer : MonoBehaviour
{
    [SerializeField] float runSpeed = 6f;
    [SerializeField] float jumpForce = 8f;
    //[SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathJump = new Vector2(0f, 10f);

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    Vector2 moveInput;

    float gravityScaleAtStart;

    bool isAlive = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }
    void Update()
    {
        if (!isAlive)
        { return; }
        Run();
        FlipSprite();
        //ClimbLadder();
        Die();
    }

    // get move input as a Vector2 when pressing "wasd" or "arrow keys"
    // running speed is applied in the "run()" method 
    private void OnMove(InputValue input)
    {
        // this if statement makes the code below it not work if conditions are met
        if (!isAlive)
        { return; }

        moveInput = input.Get<Vector2>();

        //Debug.Log(moveInput);
    }

    // check if jump button is pressed
    // and apply jump force if its pressed
    void OnJump(InputValue value)
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
        { return; }
        if (value.isPressed && isAlive)
        {
            myRigidbody.linearVelocity += new Vector2(0, jumpForce);
        }
    }


    // this is the function where running speed is applied
    void Run()
    {
        // player velocity is stored in a vector 2 here
        // x velocity is user input, y velocity is whatever its currently is
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.linearVelocity.y);
        myRigidbody.linearVelocity = playerVelocity;

        Debug.Log(playerVelocity);


        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", hasHorizontalSpeed);
    }

    void FlipSprite()
    {
        // check if player has velocity
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;

        // if player has velocity put its x values sign (-1 or +1) in its (scale) x dimension fliping info...
        // ... so if your velocity is (-) you flip to left, if its (+) you look to right side again
        if (hasHorizontalSpeed)
        {
            //transform.localScale = new Vector3 (Mathf.Sign(myRigidbody.linearVelocity.x), 1, 1);
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.linearVelocity.x), 1);
        }
    }

    //void ClimbLadder()
    //{
    //    if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
    //    {
    //        // when exiting a latter collider (in this case when not touching it) set gravity of player back to starting value
    //        myRigidbody.gravityScale = gravityScaleAtStart;
    //        // set animation trigger bools
    //        myAnimator.SetBool("IsClimbing", false);
    //        // return to exit out of the ENTIRE FUNCTION if not touching a collider with "Climbing" layer
    //        return;
    //    }
    //    Vector2 climbVelocity = new Vector2(myRigidbody.linearVelocity.x, moveInput.y * climbSpeed);
    //    myRigidbody.linearVelocity = climbVelocity;

    //    // gravity scale is set to zero when touching the latter collider so the player does not slide down
    //    myRigidbody.gravityScale = 0f;
    //    // set animation trigger bools
    //    bool hasVerticleSpeed = Mathf.Abs(myRigidbody.linearVelocity.y) > Mathf.Epsilon;
    //    myAnimator.SetBool("IsClimbing", hasVerticleSpeed);
    //    myAnimator.SetBool("IsRunning", false);
    //}

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.linearVelocity = deathJump;
        }
    }
}
