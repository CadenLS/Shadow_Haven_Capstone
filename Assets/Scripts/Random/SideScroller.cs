using UnityEngine;

public class SideScroller : MonoBehaviour
{
    public Transform cameraTransform;  // Reference to the camera's Transform
    public float scrollSpeed = 0.5f;   // Speed of the horizontal scroll

    private Material material;         // The material of the background
    private Vector3 lastCameraPosition;

    void Start()
    {
        // Get the material from the Renderer component
        material = GetComponent<Renderer>().material;

        // Store the initial camera position
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            // Calculate how much the camera has moved horizontally
            Vector3 cameraDelta = cameraTransform.position - lastCameraPosition;

            // Adjust the horizontal offset of the background texture
            float horizontalOffset = cameraDelta.x * scrollSpeed;
            material.mainTextureOffset += new Vector2(horizontalOffset, 0);

            // Update the vertical position of the background to follow the camera
            transform.position = new Vector3(transform.position.x, cameraTransform.position.y, transform.position.z);

            // Save the new camera position for the next frame
            lastCameraPosition = cameraTransform.position;
        }
    }
}
