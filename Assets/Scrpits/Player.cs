using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f; //Player speed left and right

    //Jump variables
    public int health = 100;
    public float jumpForce = 8f;
    public Transform groundCheck; //empty objects at Players feet
    public float groundCheckRadius = 0.2f; //size of the circle used to detect ground
    public LayerMask groundLayer;  //Which layer the ground is on

    //Internal variables 
    private Rigidbody2D rb;  //Reference to Player rb
    private bool isGrounded; //True if player is touching the ground
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            health -= 25;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            StartCoroutine(BlinkRed());

            if(health <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}

