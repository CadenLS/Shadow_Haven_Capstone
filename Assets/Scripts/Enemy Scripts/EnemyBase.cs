using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    private GroundEnemyMovement groundEnemyMovement;

    public float enemyHealth;
    public bool tookDamage = false;
    public float collisionDamage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        enemyHealth = 50f;
        groundEnemyMovement = GetComponent<GroundEnemyMovement>();

    }

    private void Update()
    {
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    // Method to take damage from an attack
    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        Debug.Log("Enemy took damage: " + damage + ", remaining health: " + enemyHealth);
    }

    // Method to disable the enemy object
    private void Die()
    {
        Debug.Log("Enemy died.");
        gameObject.SetActive(false);
    }

}
