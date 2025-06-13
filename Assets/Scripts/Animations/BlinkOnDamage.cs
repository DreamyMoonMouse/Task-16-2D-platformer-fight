using UnityEngine;

public class BlinkOnDamage : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private BlinkAnimation _blinkAnimation;

    private void OnEnable()
    {
        if (_health != null && _blinkAnimation != null)
        {
            _health.Damaged += HandleDamage;
        }
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            _health.Damaged -= HandleDamage;
        }
    }

    private void HandleDamage(int damageAmount)
    {
        _blinkAnimation.Blink();
    }
}
