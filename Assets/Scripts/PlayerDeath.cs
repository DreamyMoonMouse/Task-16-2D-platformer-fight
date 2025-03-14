using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D), typeof(Animations), typeof(Invulnerability))]
public class PlayerDeath : Death
{
    [SerializeField] private Canvas _restartCanvas;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Animations _animations;

    private Rigidbody2D _rigidbody;
    //private Invulnerability _invulnerability;
    
    public event Action OnPlayerDied;
    public event Action OnGameRestarted;

    protected override void Awake()
    {
        base.Awake();
        _restartCanvas.gameObject.SetActive(false);
        _rigidbody = GetComponent<Rigidbody2D>();
        //_invulnerability = GetComponent<Invulnerability>();
    }

    protected override void Die()
    {
        if (_isDead) return;

        float delay = 1.5f;
        _isDead = true;

        if (_rigidbody != null)
        {
            _rigidbody.simulated = false;
        }

        OnPlayerDied?.Invoke();
        StartCoroutine(ShowRestartButtonAfterDelay(delay));
    }

    private IEnumerator ShowRestartButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_restartCanvas != null)
        {
            _restartCanvas.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        _isDead = false;

        if (_restartCanvas != null)
        {
            _restartCanvas.gameObject.SetActive(false);
        }

        Respawn();

        if (_rigidbody != null)
        {
            _rigidbody.simulated = true;
        }

        _health.Heal(_health.MaxHealth);
        OnGameRestarted?.Invoke();
    }

    private void Respawn()
    {
        transform.position = _startPosition.position;
        transform.localScale = Vector3.one;
        _rigidbody.linearVelocity = Vector2.zero;
    }
}