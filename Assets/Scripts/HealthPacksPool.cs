using UnityEngine;
using System.Collections.Generic;

public class HealthPacksPool : MonoBehaviour
{
    [SerializeField] private PlayerDeath _playerDeath;
    [SerializeField] private HealthPackCollector _healthPackCollector;
    [SerializeField] private Health _playerHealth;

    private List<HealthPack> _healthPacks = new List<HealthPack>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            var healthPack = child.GetComponent<HealthPack>();
            if (healthPack != null)
            {
                _healthPacks.Add(healthPack);
            }
        }

        _healthPackCollector.OnHealthPackCollected += HandleHealthPackCollected;
        _playerDeath.OnGameRestarted += ResetAllHealthPacks;
    }

    private void OnDestroy()
    {
        _healthPackCollector.OnHealthPackCollected -= HandleHealthPackCollected;
        _playerDeath.OnGameRestarted -= ResetAllHealthPacks;
    }

    private void HandleHealthPackCollected(HealthPack collectedHealthPack)
    {
        _playerHealth.Heal(_playerHealth.MaxHealth);
    }

    private void ResetAllHealthPacks()
    {
        foreach (var healthPack in _healthPacks)
        {
            healthPack.ResetHealthPack();
        }
    }
}
