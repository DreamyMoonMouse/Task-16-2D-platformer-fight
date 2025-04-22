using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Death : MonoBehaviour
{
    [SerializeField] protected Health _health;
    
    protected bool _isDead = false;

    protected virtual void Awake()
    {
        _health = GetComponent<Health>();
    }

    protected virtual void Update()
    {
        if (_health.CurrentHealth <= 0 && _isDead == false)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        _isDead = true;
        gameObject.SetActive(false);
    }
}
