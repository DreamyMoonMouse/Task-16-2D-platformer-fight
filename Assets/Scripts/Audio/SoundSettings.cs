using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Toggle _soundsToggle;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _buttonsSlider;
    
    private float _minVolumeDb = -80f;
    private float _maxVolumeDb = 0f;
    private float _minSliderValue = 0f;
    private float _savedMasterSliderValue = 1f;
    private bool _isUpdatingFromCode = false;

    private void Start()
    {
        bool soundsEnabled = PlayerPrefs.GetInt("SoundsEnabled", 1) == 1;
        _soundsToggle.isOn = soundsEnabled;
        _savedMasterSliderValue = PlayerPrefs.GetFloat("MasterVolume", 1f);
        _masterSlider.value = _savedMasterSliderValue;
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        _buttonsSlider.value = PlayerPrefs.GetFloat("ButtonsVolume", 1);
        
        _masterSlider.onValueChanged.AddListener(OnMasterSliderChanged);
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        _buttonsSlider.onValueChanged.AddListener(ChangeButtonsVolume);
        
        ChangeMusicVolume(_musicSlider.value);
        ChangeButtonsVolume(_buttonsSlider.value);
        
        if (soundsEnabled)
        {
            ChangeMasterVolume(_masterSlider.value);
        }
        else
        {
            _mixer.SetFloat("MasterVolume", _minVolumeDb);
            _masterSlider.value = _minSliderValue;
        }
    }

    public void ToggleSounds(bool enabled)
    {
        if (enabled)
        {
            float db = LinearToDB(_savedMasterSliderValue);
            _mixer.SetFloat("MasterVolume", db);
            _isUpdatingFromCode = true;
            _masterSlider.value = _savedMasterSliderValue;
            _isUpdatingFromCode = false;
        }
        else
        {
            _savedMasterSliderValue = _masterSlider.value;
            _mixer.SetFloat("MasterVolume", _minVolumeDb);
            _isUpdatingFromCode = true;
            _masterSlider.value = _minSliderValue;
            _isUpdatingFromCode = false;
        }
        
        PlayerPrefs.SetInt("SoundsEnabled", enabled ? 1 : 0);
        PlayerPrefs.SetFloat("MasterVolume", _savedMasterSliderValue);
    }

    public void ChangeMasterVolume(float sliderValue)
    {
        float db = LinearToDB(sliderValue);
        _mixer.SetFloat("MasterVolume", db);
        
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }

    public void ChangeMusicVolume(float sliderValue)
    {
        float db = LinearToDB(sliderValue);
        _mixer.SetFloat("MusicVolume", db);
        
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void ChangeButtonsVolume(float sliderValue)
    {
        float db = LinearToDB(sliderValue);
        _mixer.SetFloat("ButtonsVolume", db);
        
        PlayerPrefs.SetFloat("ButtonsVolume", sliderValue);
    }
    
    private void OnMasterSliderChanged(float value)
    {
        if (_isUpdatingFromCode) return;
        
        ChangeMasterVolume(value);
        bool shouldEnableSound = value > _minSliderValue;
        
        if (_soundsToggle.isOn != shouldEnableSound)
        {
            _isUpdatingFromCode = true;
            _soundsToggle.isOn = shouldEnableSound;
            ToggleSounds(shouldEnableSound);
            _isUpdatingFromCode = false;
        }
    }
    
    private float LinearToDB(float linear)
    {
        float minValue = 0.0001f;
        float multiplyer = 20f;
        
        return linear >= minValue ? Mathf.Log10(linear) * multiplyer : _minVolumeDb;
    }
}
