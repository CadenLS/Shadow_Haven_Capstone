using UnityEngine;

public class Hover : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public DoubleJump doubleJump;

    private float hoverTicks;
    private float hoverDur = 0.5f;
    private float hoverForce = -1;
    public bool isHovering;
    public bool canHover = false;
    public double hoverAmount = 1;
    public double originalHoverAmount;

    private float originalGravity = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerMovement = GetComponent<PlayerMovement>();
        doubleJump = GetComponent<DoubleJump>();
        originalGravity = playerMovement.rb.gravityScale;
        originalHoverAmount = hoverAmount;

    }

    public void HoverAbility()
    {

        if (canHover && !playerMovement.isOnGround)
        {
            isHovering = true;
            // Apply upward force to simulate hover
            playerMovement.rb.gravityScale = 0.1f; // A small gravity value to slow down falling
            playerMovement.rb.linearVelocity = new Vector2(playerMovement.rb.linearVelocityX, hoverForce);

            // Stop hovering after a short duration
            Invoke(nameof(EndHover), hoverDur);
            --hoverAmount;
        }

    }

    private void EndHover()
    {
        // Restore gravity
        playerMovement.rb.gravityScale = originalGravity;
        isHovering = false;
        canHover = false;

        // Allow double jump again if the ability is unlocked
        if (AbilityManager.Instance.IsAbilityUnlocked("DJ") && doubleJump.jumpedTwice == false)
        {
            doubleJump.canDoubleJump = true;
        }
    }

}
