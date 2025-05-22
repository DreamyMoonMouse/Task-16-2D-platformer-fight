using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue = 100;
    
    private int _currentValue;
    
    public event Action<int> ValueChanged;

    public int CurrentValue => _currentValue;
    public int MaxValue => _maxValue;
    
    private void Awake()
    {
        _currentValue = _maxValue;
    }

    public void ApplyDamage(int amount)
    {
        if (_currentValue <= 0 || amount < 0) 
            return;

        _currentValue -= amount;
        _currentValue = Mathf.Max(_currentValue, 0);
        ValueChanged?.Invoke(_currentValue);
    }

    public void ApplyHeal(int amount)
    {
        if (amount < 0) 
            return;

        _currentValue += amount;
        _currentValue = Mathf.Min(_currentValue, _maxValue);
        ValueChanged?.Invoke(_currentValue);
    }
}