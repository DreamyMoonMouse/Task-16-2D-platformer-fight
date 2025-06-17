using UnityEngine;
using UnityEngine.UI;

public class VampirismUI : MonoBehaviour
{
    [SerializeField] private Image _fillBar; 
    [SerializeField] private Text _timerText; 

    public void UpdateTimer(float fillAmount, float remainingTime)
    {
        _fillBar.fillAmount = fillAmount; 
        
        if (_timerText != null)
            _timerText.text = Mathf.Ceil(remainingTime).ToString(); 
    }

    public void UpdateCooldown(float fillAmount, float cooldownRemaining)
    {
        _fillBar.fillAmount = fillAmount; 
        
        if (_timerText != null)
            _timerText.text = Mathf.Ceil(cooldownRemaining).ToString();
    }

    public void Show()
    {
        _fillBar.gameObject.SetActive(true);
        
        if (_timerText != null)
            _timerText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _fillBar.gameObject.SetActive(false);
        
        if (_timerText != null)
            _timerText.gameObject.SetActive(false);
    }
}
