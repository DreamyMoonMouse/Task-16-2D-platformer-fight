using UnityEngine;
using System.Collections;

public class PlayerFlipAnimation : MonoBehaviour
{
    [SerializeField] private Transform _spriteTransform;
    [SerializeField] private float _turnDuration = 0.5f;

    private bool _facingRight = true;
    private Coroutine _currentTurnCoroutine;

    private void Awake()
    {
        if (_spriteTransform == null)
        {
            _spriteTransform = GetComponentInChildren<SpriteRenderer>()?.transform;
        }
    }

    public void HandleFlip(float horizontalInput)
    {
        if (_currentTurnCoroutine != null || Mathf.Approximately(horizontalInput, 0f))
        {
            return;
        }

        if ((horizontalInput > 0 && !_facingRight) || (horizontalInput < 0 && _facingRight))
        {
            Flip(horizontalInput > 0);
        }
    }

    private void Flip(bool isTurningRight)
    {
        if (_currentTurnCoroutine != null)
        {
            StopCoroutine(_currentTurnCoroutine);
        }

        _currentTurnCoroutine = StartCoroutine(Turn(isTurningRight));
    }

    private IEnumerator Turn(bool isTurningRight)
    {
        float currentAngle = _spriteTransform.rotation.eulerAngles.y;
        float targetAngle = isTurningRight ? 0f : 180f;
        float timeElapsed = 0f;

        while (timeElapsed < _turnDuration)
        {
            float interpolationFactor = timeElapsed / _turnDuration;
            float newAngle = Mathf.Lerp(currentAngle, targetAngle, interpolationFactor);
            _spriteTransform.rotation = Quaternion.Euler(0f, newAngle, 0f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _facingRight = isTurningRight;
        _currentTurnCoroutine = null;
    }
}
