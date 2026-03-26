using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f; //Player speed left and right

    //Jump variables
    public float jumpForce = 8f;
    public int jumpCountValue = 1; //counter for the double jump (2 total)
    public int extraJump; //actual jump amount (curr 0)
    public Transform groundCheck; //empty objects at Players feet
    public float groundCheckRadius = 0.2f; //size of the circle used to detect ground
    public LayerMask groundLayer;  //Which layer the ground is on

    //Internal variables 
    private Rigidbody2D rb;  //Reference to Player rb
    private bool isGrounded; //True if player is touching the ground
    private Animator animator;  //Reference to Player animator

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //player rigidbody is encapsulated here
        animator = GetComponent<Animator>(); //player animator is encapsulated here
        extraJump = jumpCountValue; //sets the extraJump up at the game launch so it's automatically set to 1 by default
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal movement
        //get the input from keyboard A/D or left and right arrows
        float moveInput = Input.GetAxis("Horizontal");
        //apply horizontal speed 
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        //jump movement
        if (Input.GetButtonDown("Jump"))
        {
            //first jump (standard ifGrounded)
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            //second jump (every update resets the extrajump counter and brings it back to 1)
            else if (extraJump > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJump--;
                isGrounded = false;
            }
        }


        SetAnimation(moveInput);    //calls the function to set the animation based on the player's movement and grounded state
    }

    // Decides which animation to play based on the player's movement input and whether they are grounded or not
    private void SetAnimation(float moveInput)
    {
        if (isGrounded)                         // on the ground
        {
            if (moveInput == 0)                 // not moving
            {
                animator.Play("Player_Idle");   // play idle animation
            }
            else                                // moving
            {
                animator.Play("Player_Run");    //play run animation        
            }
        }
        else                                    // in the air (not grounded)
        {
            if (rb.linearVelocity.y > 0)        // ascending
            {
                animator.Play("Player_Jump");   // play jump animation
            }
            else                                // descending
            {
                animator.Play("Player_Fall");   // play fall animation
            }
        }
    }

    private void FixedUpdate()
    {
        //constant ground check restraint
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}