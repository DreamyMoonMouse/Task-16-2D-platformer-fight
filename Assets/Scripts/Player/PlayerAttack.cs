using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerAttack : Attack
{
    [SerializeField] private Animations _animations;
    
    private InputReader _inputReader; 

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _animations = GetComponent<Animations>();
        
        if (_animations == null)
            _animations = GetComponentInChildren<Animations>();
    }

    private void Update()
    {
        if (_inputReader.CheckAttackButtonPressed())
            Attack();
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