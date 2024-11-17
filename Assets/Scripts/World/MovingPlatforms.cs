using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{

    public float speed;
    public int startingPoint;
    public Transform[] points;

    private int i;
    private Vector3 lastPlatformPosition;
    private Rigidbody2D platformRb;

    void Start()
    {
        platformRb = GetComponent<Rigidbody2D>();
        transform.position = points[startingPoint].position;
        lastPlatformPosition = transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }

        // Calculate target position and use Rigidbody for movement
        Vector2 targetPosition = Vector2.MoveTowards(platformRb.position, points[i].position, speed * Time.deltaTime);
        platformRb.MovePosition(targetPosition);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 platformDelta = transform.position - lastPlatformPosition;

            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // Set platform velocity relative to the player
                playerMovement.platformVelocity = platformDelta / Time.fixedDeltaTime;
                playerMovement.isOnMovingPlatform = true;
            }
        }
    }

    private void LateUpdate()
    {
        lastPlatformPosition = transform.position;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.platformVelocity = Vector2.zero;
                playerMovement.isOnMovingPlatform = false;
            }
        }
    }

}
