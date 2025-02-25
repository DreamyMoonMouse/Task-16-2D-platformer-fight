using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 2;
    
    private int _currentHealth;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);
    }

    public void Heal(int amount)
    {
        if (amount < 0) return;

        _currentHealth += amount;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
    }
}