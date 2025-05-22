using UnityEngine;
using UnityEngine.UI;

public class SliderHealthDisplay : HealthDisplayBase
{
    [SerializeField] private Slider _slider;

    protected override void UpdateDisplay(float normalizedValue)
    {
        _slider.value = normalizedValue;
    }
}
