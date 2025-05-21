using UnityEngine;
using System;

[RequireComponent(typeof(Animations))]
public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    
    private int _currentHealth;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    
    public event Action<int> OnDamaged;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0 || damage < 0) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);
        OnDamaged?.Invoke(_currentHealth);
    }

    public void Heal(int amount)
    {
        if (amount < 0) return;

        _currentHealth += amount;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
    }
}