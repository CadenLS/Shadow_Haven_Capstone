using UnityEngine;

public class DestroyOnGrab : MonoBehaviour
{

    public string abilityAcquired;

    void Update()
    {
        
        if (AbilityManager.Instance.IsAbilityUnlocked(abilityAcquired))
        {
            Destroy(gameObject);
        }

    }

}
