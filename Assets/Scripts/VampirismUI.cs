using UnityEngine;
using UnityEngine.UI;

public class VampirismUI : MonoBehaviour
{
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private Image _cooldownFill;

    private static VampirismUI _instance;
    public static VampirismUI Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateTimer(float fillAmount)
    {
        _timerSlider.value = fillAmount;
        _timerSlider.gameObject.SetActive(true);
    }

    public void UpdateCooldown(float fillAmount)
    {
        _cooldownFill.fillAmount = fillAmount;
        _cooldownFill.gameObject.SetActive(true);
    }

    public void Show()
    {
        _timerSlider.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _timerSlider.gameObject.SetActive(false);
        _cooldownFill.gameObject.SetActive(false);
    }
}
