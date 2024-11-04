using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableBase : MonoBehaviour
{

    public string playerTag = "Player";
    public bool shouldChangeScenes = false;
    public string sceneLoaded;

    public bool shouldGainAbility = false;
    public string abilityToUnlock; // Name of the ability to unlock

    public float floatSpeed = 2f;
    public float floatHeight = 0.5f;
    private Vector3 startPosition;

    private void Start()
    {

        startPosition = transform.position;

    }

    private void Update()
    {

        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag(playerTag))
        {
            OnCollect();

            if (shouldGainAbility && !string.IsNullOrEmpty(abilityToUnlock))
            {
                AbilityManager.Instance.UnlockAbility(abilityToUnlock); // Unlock the ability globally
            }

            if (shouldChangeScenes && !string.IsNullOrEmpty(sceneLoaded))
            {
                SceneManager.LoadScene(sceneLoaded);
            }

            Destroy(gameObject);
        }

    }

    protected virtual void OnCollect()
    {
        Debug.Log("Collected " + gameObject.name);
    }

}
