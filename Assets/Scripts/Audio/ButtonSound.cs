using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonSound; 
    [SerializeField] private AudioSource _audioSource;
    
    public void OnStartButtonClicked()
    {
        _audioSource.PlayOneShot(_buttonSound);
    }
}