using UnityEngine;
using TMPro;

public class TextHealthDisplay : HealthDisplayBase
{
    [SerializeField] private TextMeshProUGUI _label;

    protected override void UpdateDisplay(float normalizedValue)
    {
        int current = Mathf.RoundToInt(normalizedValue * _health.MaxValue);
        _label.text = $"{current}/{_health.MaxValue}";
    }
}

