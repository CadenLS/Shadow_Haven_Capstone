using UnityEngine;

public class Hover : MonoBehaviour
{

    public PlayerMovement playerMovement;

    private float hoverTicks;
    private float hoverDur = 0.5f;
    private float hoverForce = -1;
    public bool canHover = false;
    public double hoverAmount = 1;
    public double originalHoverAmount;

    private float originalGravity = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerMovement = GetComponent<PlayerMovement>();
        originalGravity = playerMovement.rb.gravityScale;
        originalHoverAmount = hoverAmount;

    }

    public void HoverAbility()
    {

        if (canHover && !playerMovement.isOnGround)
        {
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

        canHover = false;
    }

}
