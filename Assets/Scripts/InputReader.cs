using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string JumpButton = "Jump";
    private const string HorizontalAxis = "Horizontal";
    
    private float _horizontalInput; 
    private bool _isJumpButtonPressed;
    
    public float HorizontalInput => _horizontalInput;
    public bool IsJumpButtonPressed => _isJumpButtonPressed;

    private void Update()
    {
        _horizontalInput = Input.GetAxis(HorizontalAxis);
        _isJumpButtonPressed = Input.GetButtonDown(JumpButton);
    }
}
