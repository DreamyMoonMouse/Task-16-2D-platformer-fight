using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] protected int _damage = 1;
    [SerializeField] private float _forceMultiplierForTarget = 1f;
    [SerializeField] private float _forceMultiplierForAttacker = 0.5f;
    
    private float _lastAttackTime;
    protected abstract bool CanAttack(Collider2D collider);

    protected void PerformAttack()
    {
        if (IsAttackReady() == false) return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _attackRange);
        TryAttackFirstValidTarget(colliders);
    }

    private bool IsAttackReady()
    {
        return Time.time - _lastAttackTime >= _attackCooldown;
    }

    private void TryAttackFirstValidTarget(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (CanAttack(collider) && TryApplyDamage(collider))
            {
                ApplyKnockback(collider);
                _lastAttackTime = Time.time;
                break; 
            }
        }
    }

    private bool TryApplyDamage(Collider2D collider)
    {
        if (collider.TryGetComponent<Health>(out Health health))
        {
            health.ApplyDamage(_damage);
            return true;
        }
        
        return false;
    }

    private void ApplyKnockback(Collider2D targetCollider)
    {
        if (targetCollider.TryGetComponent<Knockback>(out Knockback targetKnockback))
        {
            Vector2 directionToTarget = (targetCollider.transform.position - transform.position).normalized;
            targetKnockback.Apply(directionToTarget, _forceMultiplierForTarget, true);

            if (TryGetComponent<Knockback>(out Knockback attackerKnockback))
            {
                Vector2 directionToAttacker = -directionToTarget;
                attackerKnockback.Apply(directionToAttacker, _forceMultiplierForAttacker, true);
            }
        }
    }
}