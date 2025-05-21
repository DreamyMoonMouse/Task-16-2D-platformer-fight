using UnityEngine;
using TMPro;

public class TextHealthDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.Damaged += UpdateText;
    }

    private void OnDisable()
    {
        _health.Damaged -= UpdateText;
    }

    private void Start()
    {
        UpdateText(_health.CurrentValue);
    }

    private void UpdateText(int current)
    {
        _text.text = $"{current}/{_health.MaxValue}";
    }
}

