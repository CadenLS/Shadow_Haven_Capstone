using UnityEngine;

public class WallJump : MonoBehaviour
{

    public PlayerMovement playerMovement;

    public bool canWallJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerMovement = GetComponent<PlayerMovement>();
            
    }

    // Update is called once per frame
    void Update()
    {

        if (playerMovement.isAgainstWall)
        {
            // Reset double jump ability when on the ground
            canWallJump = true;

        }

    }
}
