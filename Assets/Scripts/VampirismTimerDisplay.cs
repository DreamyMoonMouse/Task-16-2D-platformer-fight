using UnityEngine;
using UnityEngine.UI;

public class VampirismTimerDisplay : MonoBehaviour
{
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private Image _cooldownImage;

    private void UpdateTimer(float fillAmount)
    {
        _timerSlider.value = fillAmount;
        _timerSlider.gameObject.SetActive(true);
    }

    private void UpdateCooldown(float fillAmount)
    {
        _cooldownImage.fillAmount = fillAmount;
        _cooldownImage.gameObject.SetActive(true);
    }

    public void Show()
    {
        _timerSlider.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _timerSlider.gameObject.SetActive(false);
        _cooldownImage.gameObject.SetActive(false);
    }
}
