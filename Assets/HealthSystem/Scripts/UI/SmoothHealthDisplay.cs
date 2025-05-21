using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SmoothHealthDisplay : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _smoothSpeed = 50f;
    [SerializeField] private Health _health;

    private Coroutine _coroutine;

    private void Start()
    {
        _slider.maxValue = _health.MaxValue;
        _slider.value = _health.CurrentValue;
    }

    private void OnEnable()
    {
        _health.Damaged += AnimateSlider;
    }

    private void OnDisable()
    {
        _health.Damaged -= AnimateSlider;
    }

    private void AnimateSlider(int value)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(AnimateRoutine(value));
    }

    private IEnumerator AnimateRoutine(int target)
    {
        while (Mathf.Approximately(_slider.value, target) == false)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, target, _smoothSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
