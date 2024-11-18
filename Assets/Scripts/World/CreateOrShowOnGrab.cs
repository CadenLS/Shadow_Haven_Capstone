using UnityEngine;

public class CreateOrShowOnGrab : MonoBehaviour
{

    public string abilityName;       
    public GameObject objectPrefab;  
    public Transform spawnLocation;  
    public GameObject existingObject;

    private bool abilityUnlocked = false;

    void Update()
    {
        // Check if the ability has been unlocked
        if (!abilityUnlocked && AbilityManager.Instance.IsAbilityUnlocked(abilityName))
        {
            abilityUnlocked = true;

            if (objectPrefab != null)
            {
                // Instantiate a new object at the specified location
                Instantiate(objectPrefab, spawnLocation.position, spawnLocation.rotation);
            }

            if (existingObject != null)
            {
                // Make the existing object visible and enable its collider
                var renderer = existingObject.GetComponent<Renderer>();
                if (renderer != null)
                    renderer.enabled = true;

                var collider = existingObject.GetComponent<Collider2D>();
                if (collider != null)
                    collider.enabled = true;
            }
        }
    }

}
