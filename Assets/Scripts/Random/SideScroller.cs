using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    public Transform cameraTransform;  // Reference to the camera's Transform
    public float[] scrollSpeeds;       // Array of scroll speeds for each layer (faster for nearer layers, slower for distant ones)

    private Material material;         // The material of the background
    private Vector3 lastCameraPosition;
    private Renderer[] renderers;      // Store the Renderers for all layers

    void Start()
    {
        // Get the Renderer components for all background layers
        renderers = GetComponentsInChildren<Renderer>();

        // Store the initial camera position
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            // Calculate how much the camera has moved horizontally
            Vector3 cameraDelta = cameraTransform.position - lastCameraPosition;

            // Loop through each background layer and update its position
            for (int i = 0; i < renderers.Length; i++)
            {
                // Get the material for this layer
                material = renderers[i].material;

                // Calculate the horizontal offset based on the scroll speed for this layer
                float horizontalOffset = cameraDelta.x * scrollSpeeds[i];

                // Adjust the texture offset of the background for this layer
                material.mainTextureOffset += new Vector2(horizontalOffset, 0);

                // Optionally, update the vertical position to follow the camera (if needed)
                renderers[i].transform.position = new Vector3(renderers[i].transform.position.x, cameraTransform.position.y, renderers[i].transform.position.z);
            }

            // Save the new camera position for the next frame
            lastCameraPosition = cameraTransform.position;
        }
    }
}
