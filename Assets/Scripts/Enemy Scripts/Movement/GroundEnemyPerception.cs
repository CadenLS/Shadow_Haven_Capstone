using System.Collections;
using UnityEngine;

public class GroundEnemyPerception : MonoBehaviour
{

    public GroundEnemyMovement groundEnemyMovement;
    public EnemyBase enemyBase;

    public Transform player; // Reference to the player's transform
    private bool playerDetected = false;
    public bool isPaused = false; // New bool to manage pause state

    public float detectionRange = 5f; // Range of the vision cone
    public float detectionAngle = 45f; // Angle of the vision cone

    public float chaseCooldown = 2f;
    private float currentChaseTime = 0f;

    public float pauseDuration = 2f;

    void Start()
    {

        groundEnemyMovement = GetComponent<GroundEnemyMovement>();
        enemyBase = GetComponent<EnemyBase>();
        
    }

    private void Update()
    {
        // Check if the player is within the vision cone
        bool isInVision = IsPlayerInVisionCone();

        if (isInVision || enemyBase.tookDamage)
        {
            // Player is in vision, reset the chase cooldown
            playerDetected = true;
            currentChaseTime = chaseCooldown;

            // Flip the enemy to face the player
            FlipToFacePlayer();

            // Start chasing the player, constrained to the ground
            Vector2 direction = new Vector2((player.position - transform.position).normalized.x, 0);
            groundEnemyMovement.StartChase(direction);
        }
        else if (playerDetected)
        {
            // Player is out of vision, start the cooldown timer
            currentChaseTime -= Time.deltaTime;

            if (currentChaseTime > 0)
            {
                // Keep chasing during cooldown period, constrained to the ground
                Vector2 direction = new Vector2((player.position - transform.position).normalized.x, 0);
                groundEnemyMovement.StartChase(direction);
            }
            else
            {
                // Stop chasing after cooldown period
                playerDetected = false;
                groundEnemyMovement.StopChase();

                // Start the pause coroutine before patrolling, only if not already paused
                if (!isPaused)
                {
                    StartCoroutine(PauseBeforePatrol());
                }
            }
        }
        else
        {
            // Player is not detected, stop the chase
            groundEnemyMovement.StopChase();
        }
    }

    private IEnumerator PauseBeforePatrol()
    {
        Debug.Log("Entering Pause State");
        isPaused = true; // Set the pause flag
        groundEnemyMovement.StopChase(); // Ensure enemy stops moving
        yield return new WaitForSeconds(pauseDuration); // Pause for the specified duration

        Debug.Log("Resuming Patrol State");
        groundEnemyMovement.ResumePatrol(); // Resume patrolling
        isPaused = false; // Reset the pause flag
    }

    private bool IsPlayerInVisionCone()
    {
        // Calculate the direction the enemy is facing
        Vector2 facingDirection = groundEnemyMovement.IsFacingRight() ? Vector2.right : Vector2.left;

        // Calculate the direction to the player
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Calculate the angle between the enemy's facing direction and the direction to the player
        float angleToPlayer = Vector2.Angle(facingDirection, directionToPlayer);

        // Check if the player is within the angle and range
        if (angleToPlayer < detectionAngle / 2f)
        {
            // Check if the player is within the detection range
            if (Vector2.Distance(transform.position, player.position) <= detectionRange)
            {
                return true;
            }
        }

        return false;
    }

    private void FlipToFacePlayer()
    {
        // Flip the enemy to face the player
        if (player.position.x > transform.position.x && !groundEnemyMovement.IsFacingRight())
        {
            groundEnemyMovement.Flip();
        }
        else if (player.position.x < transform.position.x && groundEnemyMovement.IsFacingRight())
        {
            groundEnemyMovement.Flip();
        }
    }

    // Visualize the vision cone in the editor
    private void OnDrawGizmosSelected()
    {
        // Draw the detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Calculate the facing direction
        Vector3 facingDirection = groundEnemyMovement.IsFacingRight() ? transform.right : -transform.right;
        Vector3 forward = facingDirection * detectionRange;
        float halfAngle = detectionAngle / 2f;

        // Calculate the directions for the left and right edges of the cone
        Vector3 coneLeftEdge = Quaternion.Euler(0, 0, halfAngle) * forward;
        Vector3 coneRightEdge = Quaternion.Euler(0, 0, -halfAngle) * forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + coneLeftEdge);
        Gizmos.DrawLine(transform.position, transform.position + coneRightEdge);
    }
}
