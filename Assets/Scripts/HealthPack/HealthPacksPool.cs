using UnityEngine;
using System.Collections.Generic;

public class HealthPacksPool : MonoBehaviour
{
    private List<HealthPack> _healthPacks = new();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            var healthPack = child.GetComponent<HealthPack>();
            
            if (healthPack != null)
                _healthPacks.Add(healthPack);
        }
    }
}
