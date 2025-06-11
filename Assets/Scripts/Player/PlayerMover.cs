using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader))]
[RequireComponent(typeof(GroundDetector), typeof(Knockback))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerFlipAnimation _playerFlip;
    [SerializeField] private Animations _spriteAnimations;
    
    private Rigidbody2D _rigidbody;
    private InputReader _inputReader;
    private Knockback _knockback;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();
        _groundDetector = GetComponent<GroundDetector>();
        _knockback = GetComponent<Knockback>();
    }
    
    private void Update()
    {
        if (_knockback.IsKnockback) 
            return;

        float movementInput = _inputReader.HorizontalInput;
        _spriteAnimations.SetIsMoving(Mathf.Abs(movementInput) > 0.01f);
        _playerFlip.HandleFlip(movementInput);
    }

    private void FixedUpdate()
    {
        if (_knockback.IsKnockback) 
            return;

        float movementInput = _inputReader.HorizontalInput;
        MovePlayer(movementInput);

        if (_inputReader.CheckJumpButtonPress() && _groundDetector.IsGrounded())
            Jump();
    }

    private void MovePlayer(float movementInput)
    {
        _rigidbody.linearVelocity = new Vector2(movementInput * _moveSpeed, _rigidbody.linearVelocity.y);
    }

    private void Jump()
    {
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0f);
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
