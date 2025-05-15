using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private VolumeSlider _masterSlider;
    
    private string _soundsEnabledKey = "SoundsEnabled";
    private int _enabledValue = 1;
    
    private void OnEnable()  => _toggle.onValueChanged.AddListener(OnToggleChanged);
    
    private void OnDisable() => _toggle.onValueChanged.RemoveListener(OnToggleChanged);

    private void Start()
    {
        bool enabled = PlayerPrefs.GetInt(_soundsEnabledKey, _enabledValue) == _enabledValue;
        _toggle.SetIsOnWithoutNotify(enabled);
        _masterSlider.SetInteractable(enabled);
        ApplyState(enabled);
    }
    
    private void OnToggleChanged(bool enabled)
    {
        PlayerPrefs.SetInt(_soundsEnabledKey, enabled ? _enabledValue : 0);
        PlayerPrefs.Save();
        ApplyState(enabled);
    }

    private void ApplyState(bool enabled)
    {
        if (enabled)
        {
            float saved = PlayerPrefs.GetFloat(_masterSlider.VolumePrefKey, _masterSlider.DefaultValue);
            _masterSlider.UpdateSlider(saved);
            _masterSlider.ApplyVolume(saved);
            _masterSlider.SetInteractable(true);
        }
        else
        {
            _masterSlider.UpdateSlider(_masterSlider.MinValue);
            _masterSlider.ApplyVolume(_masterSlider.MinValue);
            _masterSlider.SetInteractable(false);
        }
    }
}
