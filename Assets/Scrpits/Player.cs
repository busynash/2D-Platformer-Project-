using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f; //Player speed left and right

    //Jump variables

    public float jumpForce = 8f;
    public Transform groundCheck; //empty objects at Players feet
    public float groundCheckRadius = 0.2f; //size of the circle used to detect ground
    public LayerMask groundLayer;  //Which layer the ground is on

    //Internal variables 
    private Rigidbody2D rb;  //Reference to Player rb
    private bool isGrounded; //True if player is touching the ground


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
