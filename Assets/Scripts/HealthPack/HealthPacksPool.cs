using UnityEngine;
using System.Collections.Generic;

public class HealthPacksPool : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private Health _playerHealth;

    private List<HealthPack> _healthPacks = new List<HealthPack>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            var healthPack = child.GetComponent<HealthPack>();
            
            if (healthPack != null)
                _healthPacks.Add(healthPack);
        }

        _collector.OnItemCollected += HandleItemCollected;
    }

    private void OnDestroy()
    {
        _collector.OnItemCollected -= HandleItemCollected;
    }

    private void HandleItemCollected(ICollectable item)
    {
        if (item is HealthPack)
            _playerHealth.ApplyHeal(_playerHealth.MaxValue);
    }
}
