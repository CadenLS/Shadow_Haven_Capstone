using UnityEngine;

public class ShadowGrapple : MonoBehaviour
{

    public float grappleSpeed = 10f;
    public float maxGrappleDistance = 15f;
    public bool isGrappling = false;
    private Vector2 grapplePoint;
    public Rigidbody2D rb;
    public float swingStrength = 5f;
    public float cooldownTime = 5f;
    private float cooldownTimer = 0f;
    private bool isOnCooldown = false;


    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        // Update cooldown timer if on cooldown
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                isOnCooldown = false; // Reset cooldown
            }
        }

        if (isGrappling)
        {
            // Calculate the swing direction and apply force
            Vector2 direction = (grapplePoint - rb.position).normalized;
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, direction * grappleSpeed, Time.deltaTime * swingStrength);

            if (Vector2.Distance(rb.position, grapplePoint) < 0.5f)
            {
                isGrappling = false;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            isGrappling = false;
        }

    }

    public void Grapple()
    {
        if (isOnCooldown) return; // Exit if on cooldown

        // Get the mouse position in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calculate the distance to the grapple point
        Vector2 directionToMouse = mousePosition - (Vector2)transform.position;

        // Check if the distance to the mouse position is within the maximum grapple distance
        if (directionToMouse.magnitude <= maxGrappleDistance)
        {
            grapplePoint = mousePosition;
            isGrappling = true;
            isOnCooldown = true;
            cooldownTimer = cooldownTime;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a line to the grapple point if currently grappling
        if (isGrappling)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, grapplePoint);
            Gizmos.DrawSphere(grapplePoint, 0.2f);
        }

        // Draw the raycast for debug purposes
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * maxGrappleDistance);
    }

}
