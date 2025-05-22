using UnityEngine;

public abstract class HealthDisplayBase : MonoBehaviour
{
    [SerializeField] protected Health _health;

    private void Start()
    {
        _health.ValueChanged += OnValueChanged;
        UpdateDisplay(_health.CurrentValue / (float)_health.MaxValue);
    }

    private void OnDestroy()
    {
        _health.ValueChanged -= OnValueChanged;
    }
    
    protected abstract void UpdateDisplay(float normalizedValue);

    private void OnValueChanged(int current)
    {
        float normalized = current / (float)_health.MaxValue;
        UpdateDisplay(normalized);
    }
}
