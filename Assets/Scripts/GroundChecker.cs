using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }
}
