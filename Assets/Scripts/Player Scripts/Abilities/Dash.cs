using UnityEngine;

public class Dash : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public MeleeAttackBase meleeAttack;

    public float dashStrength = 15;
    public float dashDur = 0.2f;
    public bool canDash = true;
    private float originalGravity;
    public bool isDashing;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        meleeAttack = GetComponent<MeleeAttackBase>();
        originalGravity = playerMovement.rb.gravityScale;
    }

    public void DashForward()
    {
        if (canDash && !isDashing && !playerMovement.isAgainstWall)
        {
            Vector2 dashDirection = (meleeAttack.GetAttackPosition() - (Vector2)transform.position).normalized;
            playerMovement.rb.linearVelocity = Vector2.zero;
            if (!playerMovement.isOnGround)
            {
                playerMovement.rb.AddForce(dashDirection * dashStrength, ForceMode2D.Impulse);
            }
            else
            {
                return;
            }

            canDash = false;
            isDashing = true;

            playerMovement.rb.gravityScale = 0;

            // Stop the dash after a short duration
            Invoke(nameof(EndDash), dashDur);
        }
        else
        {
            return;
        }
    }

    private void EndDash()
    {
        // Restore gravity
        playerMovement.rb.gravityScale = originalGravity;

        // Stop all momentum
        playerMovement.rb.linearVelocity = Vector2.zero;

        isDashing = false;
    }

}
