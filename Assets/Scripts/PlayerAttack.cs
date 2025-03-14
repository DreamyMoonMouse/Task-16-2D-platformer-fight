using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Animations))]
public class PlayerAttack : Attack
{
    private InputReader _inputReader;
    private Animations _animations;
    private PlayerDeath _playerDeath;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _animations = GetComponent<Animations>();
        _playerDeath = GetComponent<PlayerDeath>();
        //_playerDeath.OnPlayerDied += ResetAttack;
    }

    private void OnDestroy()
    {
        //_playerDeath.OnPlayerDied -= ResetAttack;
    }

    private void Update()
    {
        if (_inputReader.IsAttackButtonPressed)
        {
            Attack();
        }
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

    private void ResetAttack()
    {
        CancelInvoke(nameof(StopAttack));
        _animations.SetIsAttacking(false);
    }

    protected override bool CanAttack(Collider2D collider)
    {
        return collider.TryGetComponent<Enemy>(out _);
    }
}