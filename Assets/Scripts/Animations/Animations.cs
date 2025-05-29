using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BlinkAnimation))]
public class Animations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;
    
    private BlinkAnimation _blinkAnimation;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _blinkAnimation = GetComponent<BlinkAnimation>();
    }
    
    private void OnEnable()
    {
        _health.Damaged += HandleHurt;
    }

    private void OnDisable()
    {
        _health.Damaged -= HandleHurt;
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

