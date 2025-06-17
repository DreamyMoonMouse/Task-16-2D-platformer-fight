using UnityEngine;
using System;

public class SkillState : MonoBehaviour
{
    [SerializeField] private float _activationDuration = 6f;
    [SerializeField] private float _cooldownDuration = 4f;

    private float _remainingTime;
    private float _cooldownRemaining;
    private bool _isActivated;
    private bool _isOnCooldown;

    public delegate void StateChangeHandler(float fillAmount, float time);
    public event StateChangeHandler OnActivationTick; 
    public event StateChangeHandler OnCooldownTick; 
    public event Action OnActivate;
    public event Action OnDeactivate;
    public event Action OnApply;

    public bool CanActivate 
        => _isActivated == false && _isOnCooldown == false;

    public void Activate()
    {
        if (CanActivate == false) 
            return;

        _isActivated = true;
        _remainingTime = _activationDuration;
        OnActivate?.Invoke();
    }

    private void Update()
    {
        if (_isActivated)
        {
            _remainingTime -= Time.deltaTime;
            OnActivationTick?.Invoke(_remainingTime / _activationDuration, _remainingTime);

            if (_remainingTime <= 0f)
            {
                _isActivated = false;
                _isOnCooldown = true;
                _cooldownRemaining = _cooldownDuration;
                OnDeactivate?.Invoke();
            }
            else
            {
                OnApply?.Invoke();
            }
        }
        else if (_isOnCooldown)
        {
            _cooldownRemaining -= Time.deltaTime;
            OnCooldownTick?.Invoke(1f - (_cooldownRemaining / _cooldownDuration), _cooldownRemaining);

            if (_cooldownRemaining <= 0f)
            {
                _isOnCooldown = false;
            }
        }
    }
}
