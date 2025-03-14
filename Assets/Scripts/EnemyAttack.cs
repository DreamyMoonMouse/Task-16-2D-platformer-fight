using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAttack : Attack
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _damage = _enemy.Damage; 
    }

    private void Update()
    {
        PerformAttack();
    }

    protected override bool CanAttack(Collider2D collider)
    {
        return collider.TryGetComponent<Player>(out _);
    }
}
