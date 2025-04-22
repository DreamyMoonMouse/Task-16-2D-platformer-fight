using UnityEngine;

public static class AnimatorData
{
    public static class Params
    {
        public static readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));
        public static readonly int IsDead = Animator.StringToHash(nameof(IsDead));
        public static readonly int IsHurt = Animator.StringToHash(nameof(IsHurt));
        public static readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
    }
}
