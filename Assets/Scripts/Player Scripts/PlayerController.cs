using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerMovement playerMovement;
    private MeleeAttackBase meleeAttack;
    private Dash dash;
    private DoubleJump dbJump;
    private Hover hover;
    private CloakCreep creep;
    private WallJump wallJump;
    private ShadowGrapple grapple;

    public float playerHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerMovement = GetComponent<PlayerMovement>();
        meleeAttack = GetComponent<MeleeAttackBase>();
        dash = GetComponent<Dash>();
        dbJump = GetComponent<DoubleJump>();
        hover = GetComponent<Hover>();
        creep = GetComponent<CloakCreep>();
        wallJump = GetComponent<WallJump>();
        grapple = GetComponent<ShadowGrapple>();

    }

    void Update()
    {

        if (playerHealth <= 0)
        {
            // where I play death animation and end screen
            Debug.Log("Player Dead");
        }

        // Check for mouse input to attack
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            meleeAttack.Attack();
        }

        if (AbilityManager.Instance.IsAbilityUnlocked("Grapple") && Input.GetMouseButton(1) && !grapple.isGrappling)
        {
            grapple.Grapple();
        }

        if (AbilityManager.Instance.IsAbilityUnlocked("Dash") && Input.GetKey(KeyCode.Mouse4)) 
        {
            dash.DashForward();
            dash.canDash = false;
        }

        if (AbilityManager.Instance.IsAbilityUnlocked("Hover") && Input.GetKeyDown(KeyCode.Mouse3) && !playerMovement.isOnGround && hover.hoverAmount >= 1) 
        {
            hover.canHover = true;
            hover.HoverAbility();
        }

        if (AbilityManager.Instance.IsAbilityUnlocked("Creep") && Input.GetKey(KeyCode.X) && playerMovement.isOnGround)
        {
            creep.Creep();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we collided with the enemy
        if (collision.collider.CompareTag("Enemy"))
        {
            EnemyBase enemyBase = collision.collider.GetComponent<EnemyBase>();
            if (enemyBase != null)
            {
                // Reduce player health by the enemy's collision damage
                playerHealth -= enemyBase.collisionDamage;
                Debug.Log("Player took damage! Current health: " + playerHealth);
            }
        }
    }

}
