using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Death : MonoBehaviour
{
    [SerializeField] protected Health _health;
    
    protected bool _isDead = false;

    private void Awake()
    {
        OnAwake();
    }

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnAwake()
    {
        _health = GetComponent<Health>();
    }

    protected virtual void OnUpdate()
    {
        if (_health != null && _health.CurrentValue <= 0 && !_isDead)
            Die();
    }

    protected virtual void Die()
    {
        _isDead = true;
        gameObject.SetActive(false);
    }
}
