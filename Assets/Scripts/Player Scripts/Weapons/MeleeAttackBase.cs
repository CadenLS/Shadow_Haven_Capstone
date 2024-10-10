using UnityEngine;

public class MeleeAttackBase : MonoBehaviour
{

    public float damage;
    public float attackRadius;
    public LayerMask enemyLayer;

    public virtual void Attack()
    {
        // Perform a raycast or overlap check to detect enemies within the attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(GetAttackPosition(), attackRadius, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
            if (enemyBase != null)
            {
                enemyBase.TakeDamage(damage);
            }
        }

        // Optional: Add attack animation or effects here
    }

    private Vector2 GetAttackPosition()
    {
        // Adjust the attack position based on the player's facing direction
        // Assuming the player sprite faces right by default, adjust accordingly
        float direction = transform.localScale.x; // Use the x scale to determine facing direction
        return (Vector2)transform.position + Vector2.right * direction;
    }

    // Method to visualize the attack range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 attackPosition = GetAttackPosition(); // Get the position in front of the player
        Gizmos.DrawWireSphere(attackPosition, attackRadius);
    }

}
