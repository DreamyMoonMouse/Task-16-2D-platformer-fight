using UnityEngine;

public class HealButtonHandler : HealthButtonBase
{
    [SerializeField] private int _heal = 10;
    
    protected override void HandleAction()
    {
        _health.ApplyHeal(_heal);
    }
}
