using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonSound; 
    [SerializeField] private Button _button;
    [SerializeField] private AudioMixerGroup _mixerGroup;
    
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = _mixerGroup;
        _audioSource.playOnAwake = false;
    }
    
    private void OnEnable()  => _button.onClick.AddListener(PlaySound);
    
    private void OnDisable() => _button.onClick.RemoveListener(PlaySound);

    private void PlaySound() => _audioSource.PlayOneShot(_buttonSound);
}