using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HealButtonHandler : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private int _healAmount = 10;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ApplyHeal);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ApplyHeal);
    }

    private void ApplyHeal()
    {
        _health.Heal(_healAmount);
    }
}
