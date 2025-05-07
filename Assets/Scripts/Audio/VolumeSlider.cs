using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerGroup;
    [SerializeField] private Slider _slider;
    [SerializeField] private SoundToggle _soundToggle;

    private float _minVolume = 0.0001f;
    private float _multiplier = 20f;
    private bool _isUpdatingFromCode;
    private float _minVolumeDb = -80f;
    private float _minSliderValue = 0f;

    private void Start()
    {
        float savedValue = PlayerPrefs.GetFloat(_mixerGroup.name, 1f);
        bool soundsEnabled = PlayerPrefs.GetInt("SoundsEnabled", 1) == 1;
        
        if (soundsEnabled == false && _mixerGroup.name == "Master")
        {
            savedValue = _minSliderValue;
        }

        _slider.value = savedValue;
        SetVolume(savedValue);
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        if (_isUpdatingFromCode) return;

        SetVolume(value);
        PlayerPrefs.SetFloat(_mixerGroup.name, value);
        
        if (_mixerGroup.name == "Master" && _soundToggle != null)
        {
            _soundToggle.OnMasterSliderChanged(value);
        }
    }

    public void SetVolume(float linear)
    {
        float db = linear >= _minVolume ? Mathf.Log10(linear) * _multiplier : _minVolumeDb;
        _mixerGroup.audioMixer.SetFloat(_mixerGroup.name, db);
    }

    public void UpdateSlider(float value)
    {
        _isUpdatingFromCode = true;
        _slider.value = value;
        _isUpdatingFromCode = false;
    }
}
