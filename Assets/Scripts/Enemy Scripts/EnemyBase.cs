using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    private GroundEnemyMovement groundEnemyMovement;
    private Rigidbody2D rb; // Reference to the Rigidbody2D

    public float enemyHealth;
    public bool tookDamage = false;
    public float collisionDamage;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHealth = 15f;
        groundEnemyMovement = GetComponent<GroundEnemyMovement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    private void Update()
    {
        if (enemyHealth <= 0 && !animator.GetBool("isDead")) // Avoid repeatedly triggering death animation
        {
            animator.SetBool("isDead", true); // Trigger death animation
            Die();
        }
    }

    // Method to take damage from an attack
    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        Debug.Log("Enemy took damage: " + damage + ", remaining health: " + enemyHealth);
    }

    // Method to disable the enemy object and stop movement when it dies
    private void Die()
    {
        Debug.Log("Enemy died.");

        // Freeze the enemy's movement by setting the Rigidbody2D velocity to zero
        rb.linearVelocity = Vector2.zero;

        // You can also add a delay here before disabling the gameObject, to allow the death animation to play
        // For example:
        StartCoroutine(DisableAfterDeath());
    }

    private IEnumerator DisableAfterDeath()
    {
        // Wait for the death animation to finish
        yield return new WaitForSeconds(1f); // Adjust the duration based on the animation length

        // Disable the enemy after the death animation is done
        gameObject.SetActive(false);
    }

}
