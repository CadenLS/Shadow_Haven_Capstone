using UnityEngine;

public class Dash : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Sword swordAttack;
    private float dashStrength = 10;
    private bool canDash = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        swordAttack = GetComponent<Sword>();
    }

    public void DashForward()
    {
        if (canDash)
        {
            Vector2 dashDirection = (swordAttack.GetAttackPosition() - (Vector2)transform.position).normalized;
            playerMovement.rb.linearVelocity = Vector2.zero;
            playerMovement.rb.AddForce(dashDirection * dashStrength, ForceMode2D.Impulse);
            canDash = false;
        }
        else
        {
            return;
        }

        if (playerMovement.isOnGround)
        {
            canDash = true;
        }
    }

}
