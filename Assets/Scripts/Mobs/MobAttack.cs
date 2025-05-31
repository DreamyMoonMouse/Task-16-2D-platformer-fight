using UnityEngine;

[RequireComponent(typeof(Mob))]
public class MobAttack : Attack
{
    private void Update()
    {
        PerformAttack();
    }

    protected override bool CanAttack(Collider2D collider)
    {
        return collider.TryGetComponent<Player>(out _);
    }
}
