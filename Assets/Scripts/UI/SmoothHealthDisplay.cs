using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SmoothHealthDisplay : HealthDisplayBase
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _duration = 0.3f;

    private Coroutine _routine;
    private float _startValue;
    private float _targetValue;

    protected override void UpdateDisplay(int current, int maxValue)
    {
        if (_routine != null)
            StopCoroutine(_routine);

        _startValue = _slider.value;
        _targetValue = (float)current / maxValue;;
        _routine = StartCoroutine(AnimateRoutine());
    }

    private IEnumerator AnimateRoutine()
    {
        float elapsed = 0f;
        
        while (elapsed < _duration)
        {
            float t = elapsed / _duration;
            _slider.value = Mathf.Lerp(_startValue, _targetValue, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        _slider.value = _targetValue;
    }
}
