using UnityEngine;

public interface ISkill
{
    void Activate();
    void Deactivate();
    void Apply();
}

[RequireComponent(typeof(SkillInput), typeof(SkillState), typeof(SkillUIUpdater))]
public class Skill : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _abilityComponent;

    private ISkill _ability;
    private SkillInput _skillInput;
    private SkillState _state;
    private SkillUIUpdater _uiUpdater;

    private void Awake()
    {
        _skillInput = GetComponent<SkillInput>();
        _state = GetComponent<SkillState>();
        _uiUpdater = GetComponent<SkillUIUpdater>();

        if (_abilityComponent != null && _abilityComponent is ISkill)
            _ability = (ISkill)_abilityComponent;
    }

    private void OnEnable()
    {
        _skillInput.OnSkillActivated += HandleSkillActivation;
        _state.OnActivate += _ability.Activate;
        _state.OnActivate += _uiUpdater.Show;
        _state.OnDeactivate += _ability.Deactivate;
        _state.OnDeactivate += _uiUpdater.Hide;
        _state.OnApply += _ability.Apply;
        _state.OnActivationTick += _uiUpdater.UpdateActivation;
        _state.OnCooldownTick += _uiUpdater.UpdateCooldown;
    }

    private void OnDisable()
    {
        _skillInput.OnSkillActivated -= HandleSkillActivation;
        _state.OnActivate -= _ability.Activate;
        _state.OnActivate -= _uiUpdater.Show;
        _state.OnDeactivate -= _ability.Deactivate;
        _state.OnDeactivate -= _uiUpdater.Hide;
        _state.OnApply -= _ability.Apply;
        _state.OnActivationTick -= _uiUpdater.UpdateActivation;
        _state.OnCooldownTick -= _uiUpdater.UpdateCooldown;
    }

    private void HandleSkillActivation()
    {
        if (_state.CanActivate)
            _state.Activate();
    }
}
