using System.Collections;
using UnityEngine;

public class GroundEnemyPerception : MonoBehaviour
{
    public GroundEnemyMovement groundEnemyMovement;
    public EnemyBase enemyBase;

    public Transform player; // Reference to the player's transform
    private bool playerDetected = false;
    public bool isPaused = false;

    public float detectionRange = 5f;
    public float detectionAngle = 45f;

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
        bool isInVision = IsPlayerInVisionCone();

        if (isInVision || enemyBase.tookDamage)
        {
            playerDetected = true;
            currentChaseTime = chaseCooldown;

            FlipToFacePlayer();

            Vector2 direction = new Vector2((player.position - transform.position).normalized.x, 0);
            groundEnemyMovement.StartChase(direction);
        }
        else if (playerDetected)
        {
            currentChaseTime -= Time.deltaTime;

            if (currentChaseTime > 0)
            {
                Vector2 direction = new Vector2((player.position - transform.position).normalized.x, 0);
                groundEnemyMovement.StartChase(direction);
            }
            else
            {
                playerDetected = false;
                groundEnemyMovement.StopChase();

                if (!isPaused)
                {
                    StartCoroutine(PauseBeforePatrol());
                }
            }
        }
        else
        {
            groundEnemyMovement.StopChase();
        }
    }

    private IEnumerator PauseBeforePatrol()
    {
        isPaused = true;
        groundEnemyMovement.StopChase();
        yield return new WaitForSeconds(pauseDuration);
        groundEnemyMovement.ResumePatrol();
        isPaused = false;
    }

    private bool IsPlayerInVisionCone()
    {
        Vector2 facingDirection = groundEnemyMovement.IsFacingRight() ? Vector2.right : Vector2.left;

        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        float angleToPlayer = Vector2.Angle(facingDirection, directionToPlayer);

        if (angleToPlayer < detectionAngle / 2f)
        {
            if (Vector2.Distance(transform.position, player.position) <= detectionRange)
            {
                return true;
            }
        }

        return false;
    }

    private void FlipToFacePlayer()
    {
        if (player.position.x > transform.position.x && !groundEnemyMovement.IsFacingRight())
        {
            groundEnemyMovement.Flip();
        }
        else if (player.position.x < transform.position.x && groundEnemyMovement.IsFacingRight())
        {
            groundEnemyMovement.Flip();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Vector3 facingDirection = groundEnemyMovement.IsFacingRight() ? transform.right : -transform.right;
        Vector3 forward = facingDirection * detectionRange;
        float halfAngle = detectionAngle / 2f;

        Vector3 coneLeftEdge = Quaternion.Euler(0, 0, halfAngle) * forward;
        Vector3 coneRightEdge = Quaternion.Euler(0, 0, -halfAngle) * forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + coneLeftEdge);
        Gizmos.DrawLine(transform.position, transform.position + coneRightEdge);
    }
}
