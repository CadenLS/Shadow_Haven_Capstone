using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public float enemyHealth;

    private EnemyMovement enemyMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        enemyHealth = 10f;
        enemyMovement = GetComponent<EnemyMovement>();

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
        // Optionally, you can add animations or effects here before disabling
        Debug.Log("Enemy died.");
        gameObject.SetActive(false);
    }

}
