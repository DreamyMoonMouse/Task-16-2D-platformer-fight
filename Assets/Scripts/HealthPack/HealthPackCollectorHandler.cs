using UnityEngine;

public class HealthPackCollectorHandler : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private Health _playerHealth;

    private void OnEnable()
    {
        _collector.ItemCollected += HandleCollected;
    }

    private void OnDisable()
    {
        _collector.ItemCollected -= HandleCollected;
    }

    private void HandleCollected(ICollectable item)
    {
        if (item is HealthPack)
            _playerHealth.ApplyHeal(_playerHealth.MaxValue);
    }
}
