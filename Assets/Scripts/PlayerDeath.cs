using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(Animations))]
public class PlayerDeath : Death
{
    [SerializeField] private Animations _animations;

    private Rigidbody2D _rigidbody;
    
    public event Action OnPlayerDied;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Die()
    {
        if (_isDead) return;
        
        _isDead = true;

        if (_rigidbody != null)
        {
            _rigidbody.simulated = false;
        }

        OnPlayerDied?.Invoke();
    }
}