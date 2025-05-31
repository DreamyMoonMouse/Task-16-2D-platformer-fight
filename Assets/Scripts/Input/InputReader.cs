using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string JumpButton = "Jump";
    private const string HorizontalAxis = "Horizontal";
    private const string AttackButton = "Fire1";
    
    private float _horizontalInput; 
    private bool _isJumpButtonPressed;
    private bool _isAttackButtonPressed;
    
    public float HorizontalInput => _horizontalInput;
    public bool IsJumpButtonPressed => GetBoolAsTrigger(ref _isJumpButtonPressed);
    public bool IsAttackButtonPressed => GetBoolAsTrigger(ref _isAttackButtonPressed);

    private void Update()
    {
        _horizontalInput = Input.GetAxis(HorizontalAxis);
        _isJumpButtonPressed |= Input.GetButtonDown(JumpButton);
        _isAttackButtonPressed |= Input.GetButtonDown(AttackButton);
    }
    
    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false; 
        return localValue;
    }
}
