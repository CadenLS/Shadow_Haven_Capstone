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
        spr.flipX = true; // Default to facing left
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isChasingPlayer && !groundEnemyPerception.isPaused)
        {
            animator.SetBool("isMoving", true);
            Patrol();
        }
        else if (isChasingPlayer)
        {
            animator.SetBool("isMoving", true);
            // Move towards the player
            rb.linearVelocity = new Vector2(chaseDirection.x * speed, rb.linearVelocity.y);

            // Flip the sprite based on the movement direction
            spr.flipX = chaseDirection.x < 0;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void Patrol()
    {
        Vector2 point = curPoint.position - transform.position;

        if (curPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            spr.flipX = true; // Facing left when moving towards point B
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            spr.flipX = false; // Facing right when moving towards point A
        }

        if (Vector2.Distance(transform.position, curPoint.position) < 0.5f && curPoint == pointB.transform)
        {
            curPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, curPoint.position) < 0.5f && curPoint == pointA.transform)
        {
            curPoint = pointB.transform;
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
