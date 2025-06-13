using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDeath : Death
{
    private Rigidbody2D _rigidbody;
    
    public event Action PlayerDied;

    protected override void OnAwake()
    {
        base.OnAwake();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Die()
    {
        if (_isDead) 
            return;
        
        _isDead = true;

        if (_rigidbody != null)
            _rigidbody.simulated = false;

        PlayerDied?.Invoke();
    }
}