using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));
        public static readonly int IsDead = Animator.StringToHash(nameof(IsDead));
        public static readonly int IsHurt = Animator.StringToHash(nameof(IsHurt));
    }
}
