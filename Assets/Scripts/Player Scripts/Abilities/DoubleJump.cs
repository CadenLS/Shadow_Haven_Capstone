using UnityEngine;

public class DoubleJump : MonoBehaviour
{

    public PlayerMovement playerMovement;

    public bool canDoubleJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        playerMovement = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isOnGround)
        {
            // Reset double jump ability when on the ground
            canDoubleJump = true;
        }
    }
}
