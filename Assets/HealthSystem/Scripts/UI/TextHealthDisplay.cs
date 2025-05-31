using UnityEngine;
using TMPro;

public class TextHealthDisplay : HealthDisplayBase
{
    [SerializeField] private TextMeshProUGUI _label;

    protected override void UpdateDisplay(int current, int maxValue)
    {
        _label.text = $"{current}/{maxValue}";
    }
}

