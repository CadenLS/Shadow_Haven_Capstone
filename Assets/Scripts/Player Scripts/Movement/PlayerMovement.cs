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
            transform.localScale = new Vector3(1, 1, 1); // Facing right
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Facing left
        }

        if (isOnGround || isAgainstWall)
        {
            dash.canDash = true;
            jumped = false;
            hover.hoverAmount = hover.originalHoverAmount;
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

        rb.position = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

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
                rb.AddForce(new Vector2(rb.linearVelocityX, jumpForce));
                jumped = true;
            }
            else if ((AbilityManager.Instance.IsAbilityUnlocked("DJ") && !isOnGround && doubleJump.canDoubleJump) || (AbilityManager.Instance.IsAbilityUnlocked("WJ") && isAgainstWall && wall.canWallJump))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, 0);
                rb.AddForce(new Vector2(rb.linearVelocityX, jumpForce));
                doubleJump.canDoubleJump = false;
            }
        }

    }

}
               