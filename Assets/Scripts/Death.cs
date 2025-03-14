using UnityEngine;
using System;

public abstract class Death : MonoBehaviour
{
    [SerializeField] protected Health _health;

    public event Action OnDied;
    protected bool _isDead = false;

    protected virtual void Awake()
    {
        _health = GetComponent<Health>();
    }

    protected virtual void Update()
    {
        if (_health.CurrentHealth <= 0 && !_isDead)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        _isDead = true;
        OnDied?.Invoke();
        gameObject.SetActive(false);
    }
}
