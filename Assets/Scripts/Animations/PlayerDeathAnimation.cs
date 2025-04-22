using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animations))]
public class PlayerDeathAnimation : MonoBehaviour
{
    [SerializeField] private float _fallSpeed = 2f;
    [SerializeField] private float _deathLiftHeight = 1.5f;
    [SerializeField] private float _fallBoundary = -10f;
    
    private Vector3 _originalScale;
    private Coroutine _fallCoroutine;
    private Animations _animations;

    private void Awake()
    {
        _originalScale = transform.localScale;
        _animations = GetComponent<Animations>();
    }

    private void OnEnable()
    {
        var playerDeath = GetComponentInParent<PlayerDeath>();
        
        if (playerDeath != null)
        {
            playerDeath.OnPlayerDied += HandlePlayerDied;
        }
    }

    private void OnDisable()
    {
        var playerDeath = GetComponentInParent<PlayerDeath>();
        
        if (playerDeath != null)
        {
            playerDeath.OnPlayerDied -= HandlePlayerDied;
        }
    }
    
    private void HandlePlayerDied()
    {
        PlayDeathAnimation(true);
    }
    
    private void PlayDeathAnimation(bool isDead)
    {
        if (isDead)
        {
            _animations.SetIsDead(true);
            _fallCoroutine = StartCoroutine(FallAnimation());
        }
        else
        {
            _animations.SetIsDead(false);
            
            if (_fallCoroutine != null)
            {
                StopCoroutine(_fallCoroutine);
                _fallCoroutine = null;
            }
        }
    }

    private IEnumerator FallAnimation()
    {
        transform.localScale *= 1.5f;
        transform.position += Vector3.up * _deathLiftHeight;

        yield return new WaitForSeconds(0.5f);

        while (transform.position.y > _fallBoundary)
        {
            transform.position += Vector3.down * _fallSpeed * Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = _originalScale;
        _animations.SetIsDead(false);
    }
}
