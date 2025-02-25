using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(Health), typeof(PlayerAnimation))]
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Canvas _restartCanvas;
    [SerializeField] private Transform _startPosition;
    
    private bool _isDead = false;
    private Rigidbody2D _rigidbody;
    private Health _health;
    private PlayerAnimation _playerAnimation;
    
    public event Action OnPlayerDied;

    private void Awake()
    {
        _restartCanvas.gameObject.SetActive(false);
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _health.TakeDamage(enemy.Damage);
            
            if (_health.CurrentHealth > 0 && !_isDead)
            {
                _playerAnimation.SetIsHurt(true);
            }
            else if (_health.CurrentHealth <= 0 && !_isDead)
            {
                Die();
            }
        }
    }

    private void Die()
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

    private void RestartGame()
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
        OnPlayerDied?.Invoke();
    }
    
    private void Respawn()
    {
        float size = 1f;
        transform.position = _startPosition.position;
        transform.localScale = new Vector3(size, size, size);
        _rigidbody.linearVelocity = Vector2.zero;
    }
}