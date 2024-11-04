using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    public static AbilityManager Instance;

    private HashSet<string> unlockedAbilities = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UnlockAbility(string abilityName)
    {
        unlockedAbilities.Add(abilityName);
    }

    public bool IsAbilityUnlocked(string abilityName)
    {
        return unlockedAbilities.Contains(abilityName);
    }

}
