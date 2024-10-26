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

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        groundEnemyPerception = GetComponent<GroundEnemyPerception>();

        curPoint = pointB.transform; // set cureent point to point b

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

            // Flip the sprite based on the movement direction
            spr.flipX = chaseDirection.x < 0;
        }

    }

    private void Patrol()
    {
        Vector2 point = curPoint.position - transform.position;

        if (curPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            spr.flipX = false; // Facing right when moving towards point B
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            spr.flipX = true; // Facing left when moving towards point A
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


    // Public function to start chasing the player
    public void StartChase(Vector2 direction)
    {
        isChasingPlayer = true;
        chaseDirection = direction;
    }

    // Public function to stop chasing the player and resume patrolling
    public void StopChase()
    {
        isChasingPlayer = false;
        rb.linearVelocity = Vector2.zero;
    }

    // Determines if the enemy is facing right
    public bool IsFacingRight()
    {
        return !spr.flipX;
    }

    // Method to flip the enemy's direction
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
