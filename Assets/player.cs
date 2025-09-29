using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpHeight = 5f;
    private float movement;
    public float speed = 5f;
    private bool facingRight = true;

    public int jumpCount = 0;           // Jumps used
    public int maxJump = 2;             // Max allowed jumps
  
    [SerializeField] private bool isGrounded = true;  // visible in Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        // Flip sprite
        if (movement < 0f && facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;
        }
        else if (movement > 0f && !facingRight)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJump)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Move player
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f); // reset Y before jump
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        jumpCount++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            jumpCount = 0; // ✅ reset jumps when touching ground
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }
}
