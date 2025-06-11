using UnityEngine;

public class HealthPackCollectorHandler : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private Health _playerHealth;

    private void OnEnable()
    {
        _collector.OnItemCollected += HandleCollected;
    }

    private void OnDisable()
    {
        _collector.OnItemCollected -= HandleCollected;
    }

    private void HandleCollected(ICollectable item)
    {
        if (item is HealthPack)
            _playerHealth.ApplyHeal(_playerHealth.MaxValue);
    }
}
