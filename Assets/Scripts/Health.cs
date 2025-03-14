using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 2;
    
    private int _currentHealth;
    private Animations _animations;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _animations = GetComponent<Animations>();
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);
        _animations.TriggerHurt();
    }

    public void Heal(int amount)
    {
        if (amount < 0) return;

        _currentHealth += amount;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
    }
}