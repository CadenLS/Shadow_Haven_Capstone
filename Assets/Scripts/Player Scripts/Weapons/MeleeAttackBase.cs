using UnityEngine;

public class MeleeAttackBase : MonoBehaviour
{
    public Dash dash;
    public PlayerMovement playerMovement;

    public float damage;
    public float attackRadius;
    public LayerMask enemyLayer;
    private float bounceForce = 500;
    //public bool canAttack = true;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();
    }

    public virtual void Attack()
    {
        //if (canAttack)
        //{
        if (Input.GetKey(KeyCode.W)) // W = 119
        {
            Debug.Log("Attacking upward");
            GetAttackArea();
        }
        else if (playerMovement.isOnGround == false && Input.GetKey(KeyCode.S)) // S = 115
        {
            Debug.Log("Attacking Down");
            GetAttackArea();
        }
        else
        {
            Debug.Log("Attacking");
            GetAttackArea();
        }
        //}

        //else return;

    }

    public Vector2 GetAttackPosition()
    {

        float direction = transform.localScale.x; // Use the x scale to determine facing direction

        if (Input.GetKey(KeyCode.W)) return (Vector2)transform.position + Vector2.up;
        else if (playerMovement.isOnGround == false && Input.GetKey(KeyCode.S)) return (Vector2)transform.position + Vector2.down;
        else return (Vector2)transform.position + Vector2.right * direction;
    }

    private void GetAttackArea()
    {
        // Perform a raycast or overlap check to detect enemies within the attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(GetAttackPosition(), attackRadius, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
            if (enemyBase != null)
            {
                if (playerMovement.isOnGround == false && Input.GetKey(KeyCode.S))
                {
                    playerMovement.rb.AddForce(new Vector2(playerMovement.rb.linearVelocityY, bounceForce));
                }
                enemyBase.TakeDamage(damage);
                dash.canDash = true;
            }
        }
    }

    // Method to visualize the attack range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 attackPosition = GetAttackPosition(); // Get the position in front of the player
        Gizmos.DrawWireSphere(attackPosition, attackRadius);
    }

}
