using UnityEngine;
using UnityEngine.Serialization;

public abstract class HealthDisplayBase : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void Start()
    {
        _health.ValueChanged += OnValueChanged;
        UpdateDisplay(_health.CurrentValue, _health.MaxValue);
    }

    private void OnDestroy()
    {
        _health.ValueChanged -= OnValueChanged;
    }
    
    protected abstract void UpdateDisplay(int current, int maxValue);

    private void OnValueChanged(int current)
    {
        float normalized = current / (float)_health.MaxValue;
        UpdateDisplay(current, _health.MaxValue);
    }
}
