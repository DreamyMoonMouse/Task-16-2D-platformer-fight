using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader))]
[RequireComponent(typeof(GroundChecker), typeof(Knockback))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private PlayerFlipAnimation _playerFlip;
    [SerializeField] private Animations _spriteAnimations;
    
    private Rigidbody2D _rigidbody;
    private InputReader _inputReader;
    private Knockback _knockback;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();
        _groundChecker = GetComponent<GroundChecker>();
        _knockback = GetComponent<Knockback>();
        
        if (_spriteAnimations == null)
            _spriteAnimations = GetComponentInChildren<Animations>();
        
        if (_playerFlip == null)
            _playerFlip = GetComponentInChildren<PlayerFlipAnimation>();
    }
    
    private void Start()
    {
        StartCoroutine(RunFlow());
    }

    private IEnumerator RunFlow()
    {
        bool isRunning = true;
        
        while (isRunning)
        {
            float movementInput = _inputReader.HorizontalInput;
            
            if (_knockback.IsKnockback == false)
            {
                MovePlayer(movementInput);
                _spriteAnimations.SetIsMoving(Mathf.Abs(movementInput) > 0.01f);
                _playerFlip.HandleFlip(movementInput);
            }
            
            if (_inputReader.IsJumpButtonPressed && _groundChecker.IsGrounded())
            {
                Jump();
            }
            
            yield return null;
        }
    }

    private void MovePlayer(float movementInput)
    {
        if (_knockback.IsKnockback == false)
        {
            _rigidbody.linearVelocity = new Vector2(movementInput * _moveSpeed, _rigidbody.linearVelocity.y);
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
