using System.Collections;
using UnityEngine;

public class ShadowGrapple : MonoBehaviour
{

    public float grappleSpeed = 15f;
    public float swingStrength = 5f;
    public float maxGrappleDistance = 15f;
    public bool isGrappling = false;
    private Vector2 grapplePoint;
    public Rigidbody2D rb;
    public float cooldownTime = 5f;
    private float cooldownTimer = 0f;
    private bool isOnCooldown = false;
    private PlayerMovement playerMovement;


    void Start()
    {

        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

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
            Vector2 direction = (grapplePoint - rb.position).normalized;
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, direction * grappleSpeed, Time.deltaTime * swingStrength);

            if (Vector2.Distance(rb.position, grapplePoint) < 0.5f)
            {
                EndGrapple();
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            EndGrapple();
        }

    }

    public void Grapple()
    {
        if (isOnCooldown || isGrappling) return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = mousePosition - (Vector2)transform.position;

        if (directionToMouse.magnitude <= maxGrappleDistance)
        {
            grapplePoint = mousePosition;
            isGrappling = true;
            isOnCooldown = true;
            cooldownTimer = cooldownTime;

            playerMovement.canControl = false; // Disable player control during grapple
        }
    }

    private void EndGrapple()
    {
        isGrappling = false;

        // Temporarily disable player control for smooth transition
        StartCoroutine(SmoothTransitionToMovement());
    }

    private IEnumerator SmoothTransitionToMovement()
    {
        playerMovement.canControl = false; // Temporarily disable control

        // Capture the current velocity from the grapple
        Vector2 initialVelocity = rb.linearVelocity;

        // Duration for the transition
        float transitionTime = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            // Lerp only the horizontal component for a smoother transition
            float t = elapsedTime / transitionTime;
            Vector2 targetVelocity = new Vector2(playerMovement.movement.x * playerMovement.moveSpeed, rb.linearVelocity.y);
            float newXVelocity = Mathf.Lerp(initialVelocity.x, targetVelocity.x, t);

            // Apply the blended velocity while keeping gravity's natural effect
            rb.linearVelocity = new Vector2(newXVelocity, rb.linearVelocity.y);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Re-enable player control
        playerMovement.canControl = true;
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
