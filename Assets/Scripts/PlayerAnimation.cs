using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BlinkAnimation))]
public class PlayerAnimation : MonoBehaviour
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
        _animator.SetBool(PlayerAnimatorData.Params.IsMoving, isMoving);
    }

    public void SetIsDead(bool isDead)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsDead, isDead);
    }
    
    public void SetIsHurt(bool isHurt)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsHurt, isHurt);
        
        if (isHurt)
        {
            _blinkAnimation.Blink();
        }
    }
}