using UnityEngine;

public class DamageButtonHandler : HealthButtonBase
{
    [SerializeField] private int _damage = 10;
    
    protected override void HandleAction()
    {
        _health.ApplyDamage(_damage);
    }
}
