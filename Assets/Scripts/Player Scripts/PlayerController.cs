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

    }

    // Update is called once per frame
    void Update()
    {

        // Check for mouse input to attack
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            meleeAttack.Attack(); // Call the attack method from MeleeAttackBase
        }

        if (Input.GetKey(KeyCode.Mouse4))
        {
            dash.DashForward();
            dash.canDash = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse3) && !playerMovement.isOnGround && hover.hoverAmount >= 1)
        {
            hover.canHover = true;
            hover.HoverAbility();
        }

        if (Input.GetKey(KeyCode.X) && playerMovement.isOnGround)
        {
            creep.Creep();
        }

    }
}
