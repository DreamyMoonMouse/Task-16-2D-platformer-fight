using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] protected int _damage = 1;
    
    private float _lastAttackTime;
    protected abstract bool CanAttack(Collider2D collider);

    protected void PerformAttack()
    {
        if (Time.time - _lastAttackTime < _attackCooldown) return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _attackRange);
        
        foreach (Collider2D collider in colliders)
        {
            if (CanAttack(collider))
            {
                if (collider.TryGetComponent<Health>(out Health health))
                {
                    health.TakeDamage(_damage);
                    _lastAttackTime = Time.time;

                    if (collider.TryGetComponent<Knockback>(out Knockback targetKnockback))
                    {
                        Vector2 directionToTarget = (collider.transform.position - transform.position).normalized;
                        targetKnockback.Apply(directionToTarget, 1f, true);
                        
                        if (TryGetComponent<Knockback>(out Knockback attackerKnockback))
                        {
                            Vector2 directionToAttacker = -directionToTarget;
                            attackerKnockback.Apply(directionToAttacker, 0.5f, true);
                        }
                    }
                }
                
                break; 
            }
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}