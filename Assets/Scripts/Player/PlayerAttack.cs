using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerAttack : Attack
{
    [SerializeField] private Animations _animations;
    [SerializeField] private VampirismAbility _vampirism;
    
    private InputReader _inputReader; 

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        if (_inputReader.CheckAttackButtonPressed())
            Attack();
        
        if (_inputReader.CheckVampirismButtonPressed() && _vampirism.IsReady())
            _vampirism.Activate();
    }
    
    protected override bool CanAttack(Collider2D collider)
    {
        return collider.TryGetComponent<Mob>(out _);
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