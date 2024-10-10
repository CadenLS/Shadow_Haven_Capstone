using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    private Rigidbody2D rb; // Rigidbody2D component reference
    private Vector2 movement; // Store input

    // Basic Variables
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;

    private bool isOnGround;


    private void Start()
    {

        // Gets component in case not there
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {

        // Gets input from Input Manager
        movement.x = Input.GetAxisRaw("Horizontal");

        isOnGround = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, LayerMask.GetMask("Ground"));

        //Debug.Log("Is on ground: " + isOnGround);

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rb.AddForce(new Vector2(rb.linearVelocityX, jumpForce));
        }

        // Flip the player sprite based on movement direction
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Facing right
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Facing left
        }

    }

    private void FixedUpdate()
    {

        rb.position = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

    }

    private void OnDrawGizmos()
    {

        // Set gizmo color to blue
        Gizmos.color = Color.blue;

        // Draw a wire sphere at the groundCheck position to represent the ground check radius
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

    }

}
