using UnityEngine;

public abstract class HealthDisplayBase : MonoBehaviour
{
    [SerializeField] protected Health Health;

    private void Start()
    {
        Health.ValueChanged += OnValueChanged;
        UpdateDisplay(Health.CurrentValue / (float)Health.MaxValue);
    }

    private void OnDestroy()
    {
        Health.ValueChanged -= OnValueChanged;
    }
    
    protected abstract void UpdateDisplay(float normalizedValue);

    private void OnValueChanged(int current)
    {
        float normalized = current / (float)Health.MaxValue;
        UpdateDisplay(normalized);
    }
}
