using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DamageButtonHandler : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private int _damageAmount = 10;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ApplyDamage);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ApplyDamage);
    }

    private void ApplyDamage()
    {
        _health.TakeDamage(_damageAmount);
    }
}
