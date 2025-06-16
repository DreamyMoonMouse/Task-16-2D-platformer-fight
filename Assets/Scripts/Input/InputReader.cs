using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string JumpButton = "Jump";
    private const string HorizontalAxis = "Horizontal";
    private const string AttackButton = "Fire1";
    private const string VampirismButton = "Fire2";
    
    private float _horizontalInput; 
    private bool _isJumpButtonPressed;
    private bool _isAttackButtonPressed;
    private bool _isVampirismButtonPressed;
    
    public float HorizontalInput => _horizontalInput;

    private void Update()
    {
        _horizontalInput = Input.GetAxis(HorizontalAxis);
        _isJumpButtonPressed |= Input.GetButtonDown(JumpButton);
        _isAttackButtonPressed |= Input.GetButtonDown(AttackButton);
        _isVampirismButtonPressed |= Input.GetButtonDown(VampirismButton);
    }
    
    public bool CheckJumpButtonPress()
    {
       return GetBoolAsTrigger(ref _isJumpButtonPressed);
    }
    
    public bool CheckAttackButtonPressed()
    {
        return GetBoolAsTrigger(ref _isAttackButtonPressed);
    }
    
    public bool CheckVampirismButtonPressed()
    {
        return GetBoolAsTrigger(ref _isVampirismButtonPressed);
    }
    
    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false; 
        return localValue;
    }
}
