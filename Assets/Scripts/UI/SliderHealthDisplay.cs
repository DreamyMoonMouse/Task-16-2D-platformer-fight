using UnityEngine;
using UnityEngine.UI;

public class SliderHealthDisplay : HealthDisplayBase
{
    [SerializeField] private Slider _slider;

    protected override void UpdateDisplay(int current, int maxValue)
    {
        _slider.value = (float)current / maxValue;;
    }
}
