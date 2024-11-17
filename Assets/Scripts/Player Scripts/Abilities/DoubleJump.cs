using UnityEngine;

public class DoubleJump : MonoBehaviour
{

    public PlayerMovement playerMovement;

    public bool canDoubleJump = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        playerMovement = GetComponent<PlayerMovement>();

    }

    void Update()
    {

        if (AbilityManager.Instance.IsAbilityUnlocked("DJ") && playerMovement.isOnGround)
        {
            canDoubleJump = true;
        }

    }
}
