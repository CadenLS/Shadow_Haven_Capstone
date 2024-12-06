using UnityEngine;

public class GroundEnemyMovement : MonoBehaviour
{
    public GroundEnemyPerception groundEnemyPerception;

    public GameObject pointA;
    public GameObject pointB;
    private Transform curPoint;

    private Rigidbody2D rb;
    public SpriteRenderer spr;

    public float speed;

    private bool isChasingPlayer = false;
    private Vector2 chaseDirection;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundEnemyPerception = GetComponent<GroundEnemyPerception>();

        curPoint = pointB.transform; // Set current point to point B
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isChasingPlayer && !groundEnemyPerception.isPaused)
        {
            // Patrol behavior when not chasing the player
            Patrol();
        }
        else if (isChasingPlayer)
        {
            // Move towards the player
            rb.linearVelocity = new Vector2(chaseDirection.x * speed, rb.linearVelocity.y);

            // Flip the sprite to face the player direction
            FlipSpriteToFacePlayer();
        }
    }

    private void Patrol()
    {
        Vector2 point = curPoint.position - transform.position;

        if (curPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            FlipSpriteIfNeeded(1); // Moving towards point B
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            FlipSpriteIfNeeded(-1); // Moving towards point A
        }

        // Change the patrol point when close
        if (Vector2.Distance(transform.position, curPoint.position) < 0.5f && curPoint == pointB.transform)
        {
            curPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, curPoint.position) < 0.5f && curPoint == pointA.transform)
        {
            curPoint = pointB.transform;
        }
    }

    private void FlipSpriteIfNeeded(float moveDirection)
    {
        // Only flip the sprite if needed (flip to the correct patrol direction)
        if (moveDirection < 0 && !spr.flipX) // Facing left
        {
            spr.flipX = true;
        }
        else if (moveDirection > 0 && spr.flipX) // Facing right
        {
            spr.flipX = false;
        }
    }

    private void FlipSpriteToFacePlayer()
    {
        // Check if the player is to the right or left of the enemy and flip accordingly
        if (chaseDirection.x < 0 && !spr.flipX) // Player is on the left
        {
            spr.flipX = true;
        }
        else if (chaseDirection.x > 0 && spr.flipX) // Player is on the right
        {
            spr.flipX = false;
        }
    }

    public void ResumePatrol()
    {
        isChasingPlayer = false; // Ensure it's not chasing
        rb.linearVelocity = Vector2.zero;
    }

    public void StartChase(Vector2 direction)
    {
        isChasingPlayer = true;
        chaseDirection = direction;

        // Flip the sprite based on the chase direction
        FlipSpriteToFacePlayer();
    }

    public void StopChase()
    {
        isChasingPlayer = false;
        rb.linearVelocity = Vector2.zero;
    }

    public bool IsFacingRight()
    {
        return !spr.flipX;
    }

    public void Flip()
    {
        spr.flipX = !spr.flipX;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
