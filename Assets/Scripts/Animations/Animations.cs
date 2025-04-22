using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BlinkAnimation))]
public class Animations : MonoBehaviour
{
    private Animator _animator;
    private BlinkAnimation _blinkAnimation;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _blinkAnimation = GetComponent<BlinkAnimation>();
    }
    
    public void SetIsMoving(bool isMoving)
    {
        _animator.SetBool(AnimatorData.Params.IsMoving, isMoving);
    }

    public void SetIsDead(bool isDead)
    {
        _animator.SetBool(AnimatorData.Params.IsDead, isDead);
    }
    
    public void TriggerHurt()
    {
        _animator.SetTrigger(AnimatorData.Params.IsHurt);
        _blinkAnimation.Blink();
    }
    
    public void SetIsAttacking(bool isAttacking)
    {
        _animator.SetBool(AnimatorData.Params.IsAttacking, isAttacking);
    }
}