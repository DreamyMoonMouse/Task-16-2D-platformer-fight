using UnityEngine;

public interface ISkill
{
    void Activate();
    void Deactivate();
    void Apply();
}

[RequireComponent(typeof(SkillInput), typeof(SkillState))]
public class Skill : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _abilityComponent;
    [SerializeField] private VampirismUI _ui;
    
    private ISkill _ability;
    private SkillInput _skillInput;
    private SkillState _state;

    private void Awake()
    {
        _skillInput = GetComponent<SkillInput>();
        _state = GetComponent<SkillState>();
        _ability = (ISkill)_abilityComponent;
    }

    private void OnEnable()
    {
        _skillInput.OnSkillActivated += HandleSkillActivation;
        _state.OnActivate += _ability.Activate;
        _state.OnActivate += _ui.SetVisible;
        _state.OnDeactivate += _ability.Deactivate;
        _state.OnApply += _ability.Apply;
        _state.OnActivationTick += _ui.UpdateTimer;
        _state.OnCooldownTick += _ui.UpdateCooldown;
        _state.OnCooldownComplete += _ui.SetInvisible;
    }

    private void OnDisable()
    {
        _skillInput.OnSkillActivated -= HandleSkillActivation;
        _state.OnActivate -= _ability.Activate;
        _state.OnActivate -= _ui.SetVisible;
        _state.OnDeactivate -= _ability.Deactivate;
        _state.OnApply -= _ability.Apply;
        _state.OnActivationTick -= _ui.UpdateTimer;
        _state.OnCooldownTick -= _ui.UpdateCooldown;
        _state.OnCooldownComplete -= _ui.SetInvisible;
    }

    private void HandleSkillActivation()
    {
        if (_state.CanActivate)
            _state.Activate();
    }
}
