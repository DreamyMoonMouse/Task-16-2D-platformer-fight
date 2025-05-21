using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BlinkAnimation),typeof(Health))]
public class Animations : MonoBehaviour
{
    private Animator _animator;
    private BlinkAnimation _blinkAnimation;
    private Health _health;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _blinkAnimation = GetComponent<BlinkAnimation>();
        _health = GetComponent<Health>();
    }
    
    private void OnEnable()
    {
        _health.OnDamaged += HandleHurt;
    }

    private void OnDisable()
    {
        _health.OnDamaged -= HandleHurt;
    }
    
    public void SetIsMoving(bool isMoving)
    {
        _animator.SetBool(AnimatorData.Params.IsMoving, isMoving);
    }

    public void SetIsDead(bool isDead)
    {
        _animator.SetBool(AnimatorData.Params.IsDead, isDead);
    }
    
    public void SetIsAttacking(bool isAttacking)
    {
        _animator.SetBool(AnimatorData.Params.IsAttacking, isAttacking);
    }
    
    private void HandleHurt(int health)
    {
        _blinkAnimation.Blink();
    }
}