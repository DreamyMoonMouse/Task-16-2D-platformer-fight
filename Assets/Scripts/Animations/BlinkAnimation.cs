using UnityEngine;
using System.Collections;

public class BlinkAnimation : MonoBehaviour
{
    [SerializeField] private float _blinkDuration = 1f;
    [SerializeField] private float _blinkInterval = 0.2f;
    
    private SpriteRenderer _spriteRenderer;
    private Coroutine _blinkCoroutine;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Blink()
    {
        if (_spriteRenderer == null) 
            return;
        
        if (_blinkCoroutine != null)
            StopCoroutine(_blinkCoroutine);
        
        _blinkCoroutine = StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < _blinkDuration)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(_blinkInterval);
            elapsedTime += _blinkInterval;
        }
        
        _spriteRenderer.enabled = true;
        _blinkCoroutine = null;
    }
}
