using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class SkillInput : MonoBehaviour
{
    private InputReader _inputReader;

    public delegate void SkillActivationHandler();
    public event SkillActivationHandler OnSkillActivated;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        if (_inputReader.CheckVampirismButtonPressed())
        {
            OnSkillActivated?.Invoke();
        }
    }
}
