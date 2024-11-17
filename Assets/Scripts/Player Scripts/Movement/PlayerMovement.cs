using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    public Rigidbody2D rb; // Rigidbody2D component reference
    private Vector2 movement; // Store input
    private Dash dash;
    private DoubleJump doubleJump;
    private Hover hover;
    private WallJump wall;
    private PlayerController playerController;

    // Basic Variables
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public Transform wallCheck;
    public float wallCheckRadius = 0.1f;
    public float wallSlideSpeed = 2f;

    public bool isOnGround;
    public bool isAgainstWall;
    public bool jumped = false;
    private bool canControl = true;


    private void Start()
    {

        // Gets component in case not there
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        dash = GetComponent<Dash>();
        doubleJump = GetComponent<DoubleJump>();
        hover = GetComponent<Hover>();
        wall = GetComponent<WallJump>();

    }

    private void Update()
    {

        // Gets input from Input Manager
        movement.x = Input.GetAxisRaw("Horizontal");

        isOnGround = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, LayerMask.GetMask("Ground"));
        isAgainstWall = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckRadius, LayerMask.GetMask("Wall"));

        //Debug.Log("Is on ground: " + isOnGround);

        Jump();

        // Flip the player sprite based on movement direction
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Facing right
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Facing left
        }

        if (isOnGround || isAgainstWall)
        {
            dash.canDash = true;
            jumped = false;
            hover.hoverAmount = hover.originalHoverAmount;
        }

        if (isAgainstWall)
        {
            doubleJump.canDoubleJump = false;
        }

        // Wall sliding logic
        if (isAgainstWall && !isOnGround && rb.linearVelocityY < 0)
        {
            // If the player is against a wall and falling, reduce the fall speed
            rb.linearVelocity = new Vector2(rb.linearVelocityX, -wallSlideSpeed);
        }

    }

    private void FixedUpdate()
    {

        //rb.position = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        if (!isAgainstWall && canControl) // Prevent overriding while sliding or attached to a wall
        {
            rb.position = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
            Vector2 targetVelocity = new Vector2(movement.x * moveSpeed / 5, rb.linearVelocity.y);
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, 0.1f); // Smooth transition to target velocity
        }

    }

    private void OnDrawGizmos()
    {

        // Set gizmo color to blue
        Gizmos.color = Color.blue;

        // Draw a wire sphere at the groundCheck position to represent the ground check radius
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);

    }

    public void Jump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (isOnGround)
            {
                // Regular jump from the ground
                rb.AddForce(new Vector2(rb.linearVelocityX, jumpForce));
                jumped = true;
            }
            else if (!isOnGround && doubleJump.canDoubleJump)
            {
                // Double jump logic
                rb.linearVelocity = new Vector2(rb.linearVelocityX, 0);
                rb.AddForce(new Vector2(rb.linearVelocityX, jumpForce));
                doubleJump.canDoubleJump = false;
            }
            else if (isAgainstWall && wall.canWallJump)
            {
                // Wall jump logic: Push off the wall with a slight horizontal force
                rb.linearVelocity = Vector2.zero; // Reset velocity to avoid accumulation

                // Apply jump force with a small horizontal push
                float pushDirection = Mathf.Sign(transform.position.x - wallCheck.position.x); // Determine wall side
                rb.AddForce(new Vector2(pushDirection * jumpForce / 1.5f, jumpForce)); // Horizontal and vertical force

                StartCoroutine(DisableControlForSeconds(0.15f));

                jumped = true;
            }
        }

    }

    private IEnumerator DisableControlForSeconds(float seconds)
    {
        canControl = false; // Disable control
        yield return new WaitForSeconds(seconds); // Wait for the specified interval
        canControl = true; // Re-enable control

        if (AbilityManager.Instance.IsAbilityUnlocked("DJ"))
        {
            doubleJump.canDoubleJump = true;
        }
    }


}
               