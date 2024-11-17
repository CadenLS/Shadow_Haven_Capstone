using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector3 targetPoint = Vector3.zero;
    public PlayerMovement player;

    public float moveSpeed;
    public float lookAheadDistance = 5f, lookAheadSpeed = 3f;
    private float lookOffset;

    private bool isFalling;
    private bool isRising;
    public float maxVertOffset = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPoint = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    void LateUpdate()
    {
        if (player.isOnGround)
        {
            targetPoint.y = player.transform.position.y;
        }

        if (transform.position.y - player.transform.position.y > maxVertOffset)
        {
            isFalling = true;
        }

        if (isFalling)
        {
            targetPoint.y = player.transform.position.y;

            if (player.isOnGround)
            {
                isFalling = false;
            }
        }

        if (player.transform.position.y - transform.position.y > maxVertOffset)
        {
            isRising = true;
        }

        if (isRising)
        {
            targetPoint.y = player.transform.position.y;

            if (player.rb.linearVelocity.y <= 0) // Stop rising if player stops moving upward
            {
                isRising = false;
            }
        }

        if (player.rb.linearVelocity.x > 0f)
        {
            lookOffset = Mathf.Lerp(lookOffset, lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }
        if (player.rb.linearVelocity.x < 0f)
        {
            lookOffset = Mathf.Lerp(lookOffset, -lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }

        targetPoint.x = player.transform.position.x + lookOffset;

        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);

    }

}
