using UnityEngine;
using UnityEngine.UI;

public class SliderHealthDisplay : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;

    private void Start()
    {
        _slider.maxValue = _health.MaxValue;
        _slider.value = _health.CurrentValue;
    }

    private void OnEnable()
    {
        _health.Damaged += UpdateSlider;
    }

    private void OnDisable()
    {
        _health.Damaged -= UpdateSlider;
    }

    private void UpdateSlider(int value)
    {
        _slider.value = value;
    }
}
