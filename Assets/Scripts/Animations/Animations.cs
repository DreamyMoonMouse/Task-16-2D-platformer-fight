using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Animations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
}

