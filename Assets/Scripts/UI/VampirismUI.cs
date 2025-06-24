using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VampirismUI : MonoBehaviour
{
    [SerializeField] private Slider _progressSlider; 
    [SerializeField] private TextMeshProUGUI _timerText; 

    public void UpdateTimer(float fillAmount, float remainingTime)
    {
        _progressSlider.value = fillAmount; 
        _timerText.text = Mathf.CeilToInt(remainingTime).ToString();
    }

    public void UpdateCooldown(float fillAmount, float cooldownRemaining)
    {
        _progressSlider.value = fillAmount; 
        _timerText.text = Mathf.CeilToInt(cooldownRemaining).ToString();
    }

    public void SetVisible()
    {
        _progressSlider.gameObject.SetActive(true);
        _timerText.gameObject.SetActive(true);
    }

    public void SetInvisible()
    {
        _progressSlider.gameObject.SetActive(false);
        _timerText.gameObject.SetActive(false);
    }
}
