using UnityEngine;

public class CloakCreep : MonoBehaviour
{

    public PlayerMovement playerMovement;

    public float postCreepInvulnerability = 0.3f;
    public float creepDamage = 30;
    public float creepSpeed = 5f;
    private float creepDuration = 0.5f;

    private bool isCreeping = false;
    private bool isInvulnerable = false;
    private Vector2 creepDirection;
    private float creepCooldown = 5f;
    private float lastCreepTime;

    public bool canCreep = true;

    void Start()
    {
        
        playerMovement = GetComponent<PlayerMovement>();
        lastCreepTime = -creepCooldown; // Initialize to allow immediate use

    }

    public void Creep()
    {
        // Check if enough time has passed since the last use
        if (Time.time >= lastCreepTime + creepCooldown && !isCreeping)
        {
            // Disable player control
            isCreeping = true;
            canCreep = false; // Prevent using the ability again until cooldown is finished
            playerMovement.enabled = false;

            // Determine the creep direction based on the player's facing direction
            creepDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

            // Start the creep movement
            Invoke(nameof(CreepAttack), creepDuration);
            Invoke(nameof(EndCreep), creepDuration);

            // Make the player invulnerable
            isInvulnerable = true;
            Invoke(nameof(EndInvulnerability), creepDuration + postCreepInvulnerability);

            // Update the last creep time to the current time
            lastCreepTime = Time.time;

            // Cooldown will be reset based on the lastCreepTime check
        }

    }

    private void CreepAttack()
    {
        // Calculate the creep attack distance
        float attackDistance = creepSpeed * creepDuration;

        // Determine the center of the attack area, which is the player's current position
        Vector2 attackCenter = (Vector2)transform.position;

        // Define the size of the attack box (length centered on the player, height of 1)
        Vector2 boxSize = new Vector2(attackDistance, 1f);

        // Check for enemies in the attack area using an OverlapBox
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackCenter, boxSize, 0f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                // Apply damage to the enemy
                EnemyBase enemy = hit.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.TakeDamage(creepDamage);
                    Debug.Log($"Enemy hit for {creepDamage}");
                }
            }
        }
    }

    private void EndCreep()
    {
        // Re-enable player control
        isCreeping = false;
        playerMovement.enabled = true;
    }

    private void EndInvulnerability()
    {
        isInvulnerable = false;
    }
    private void Update()
    {
        if (isCreeping)
        {
            // Move the player automatically in the creep direction
            transform.position += (Vector3)(creepDirection * creepSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isInvulnerable && collision.collider.CompareTag("Enemy"))
        {
            // Prevent damage or knockback if the player is invulnerable
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    private void OnDrawGizmos()
    {
        if (isCreeping || canCreep)
        {
            // Set Gizmo color to red for visibility
            Gizmos.color = Color.red;

            // Calculate the creep attack distance
            float attackDistance = creepSpeed * creepDuration;

            // Determine the center of the attack range, which will be centered on the player
            Vector2 attackCenter = (Vector2)transform.position;

            // Draw a wire cube to represent the attack range, centered on the player
            Gizmos.DrawWireCube(attackCenter, new Vector3(attackDistance, 1f, 0f));
        }
    }

}
