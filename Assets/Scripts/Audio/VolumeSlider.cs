using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private const float DbMultiplier = 20f;
    private const float MinDecibels = -80f;
    private const int EnabledValue = 1;
    
    [SerializeField] private AudioMixerGroup _mixerGroup;
    [SerializeField] private Slider _slider;
    
    private string _masterVolumeKey = "Master";
    private string _soundsEnabledKey = "SoundsEnabled";
    private float _defaultValue = 1f;
    private float _minValue = 0.0001f;
    private bool _isUpdating;
    
    public string VolumePrefKey => _mixerGroup.name == _masterVolumeKey ? _masterVolumeKey : _mixerGroup.name;
    public float  DefaultValue => _defaultValue;
    public float  MinValue => _minValue;
    
    private void OnEnable() =>  
        _slider.onValueChanged.AddListener(OnSliderChanged);

    private void OnDisable() => 
        _slider.onValueChanged.RemoveListener(OnSliderChanged);

    private void Start()
    {
        float saved = PlayerPrefs.GetFloat(VolumePrefKey, _defaultValue);
        bool enabled = _mixerGroup.name != _masterVolumeKey || PlayerPrefs.GetInt(_soundsEnabledKey, EnabledValue) == EnabledValue;
        float initial = enabled ? saved : _minValue;
        UpdateSlider(initial);
        ApplyVolume(initial);
    }
    
    public void UpdateSlider(float value)
    {
        _isUpdating = true;
        _slider.value = value;
        _isUpdating = false;
    }
    
    public void SetInteractable(bool interactable)  => _slider.interactable = interactable;
    
    public void ApplyVolume(float linear)
        {
            float db = linear >= _minValue ? Mathf.Log10(linear) * DbMultiplier : MinDecibels;
            _mixerGroup.audioMixer.SetFloat(_mixerGroup.name, db);
            
            if (_mixerGroup.name == _masterVolumeKey)
                AudioListener.volume = linear;
        }

    private void OnSliderChanged(float linear)
    {
        if (_isUpdating) return;
        
        bool enabled = PlayerPrefs.GetInt(_soundsEnabledKey, EnabledValue) == EnabledValue;
        
        if (enabled==false)
        {
            UpdateSlider(_minValue);
            return;
        }
        
        ApplyVolume(linear);
        PlayerPrefs.SetFloat(VolumePrefKey, linear);
        PlayerPrefs.Save();
    }
}
