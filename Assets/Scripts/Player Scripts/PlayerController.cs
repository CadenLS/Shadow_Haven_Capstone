using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerMovement playerMovement;
    private MeleeAttackBase meleeAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerMovement = GetComponent<PlayerMovement>();
        meleeAttack = GetComponent<MeleeAttackBase>();

    }

    // Update is called once per frame
    void Update()
    {

        // Example control to disable player movement if the game is paused or player is dead
        //if (/* your pause/dead condition */)
        //{
        //    playerMovement.enabled = false; // Disable player movement
        //}
        //else
        //{
        //    playerMovement.enabled = true; // Enable player movement
        //}

        // Check for mouse input to attack
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            meleeAttack.Attack(); // Call the attack method from MeleeAttackBase
        }

    }
}
