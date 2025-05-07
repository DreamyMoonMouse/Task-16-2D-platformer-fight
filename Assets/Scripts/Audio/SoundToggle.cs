using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private VolumeSlider _masterSlider;

    private const string SoundsEnabledKey = "SoundsEnabled";
    
    private float _minVolumeDb = -80f;
    private float _minSliderValue = 0f;

    private void Start()
    {
        bool soundsEnabled = PlayerPrefs.GetInt(SoundsEnabledKey, 1) == 1;
        _toggle.isOn = soundsEnabled;
        
        if (soundsEnabled)
        {
            RestoreVolume();
        }
        else
        {
            Mute();
        }

        _toggle.onValueChanged.AddListener(ToggleSounds);
    }

    public void OnMasterSliderChanged(float value)
    {
        bool isUnmuted = value > _minSliderValue;
        
        if (_toggle.isOn != isUnmuted)
        {
            _toggle.isOn = isUnmuted;
            ToggleSounds(isUnmuted);
        }
    }

    private void ToggleSounds(bool enabled)
    {
        if (enabled)
        {
            RestoreVolume();
        }
        else
        {
            Mute();
        }

        PlayerPrefs.SetInt(SoundsEnabledKey, enabled ? 1 : 0);
    }

    private void Mute()
    {
        _masterSlider.UpdateSlider(_minSliderValue);
        _masterSlider.SetVolume( _minVolumeDb);
    }

    private void RestoreVolume()
    {
        float master = PlayerPrefs.GetFloat("Master", 1f);
        _masterSlider.UpdateSlider(master);
        _masterSlider.SetVolume(master);
    }
}
