using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Animations))]
public class PlayerAttack : Attack
{
    private InputReader _inputReader;
    private Animations _animations;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _animations = GetComponent<Animations>();
    }

    private void Update()
    {
        if (_inputReader.IsAttackButtonPressed)
        {
            Attack();
        }
    }
    
    protected override bool CanAttack(Collider2D collider)
    {
        return collider.TryGetComponent<Enemy>(out _);
    }

    private void Attack()
    {
        _animations.SetIsAttacking(true);
        PerformAttack();
        Invoke(nameof(StopAttack), 0.3f);
    }

    private void StopAttack()
    {
        _animations.SetIsAttacking(false);
    }
}