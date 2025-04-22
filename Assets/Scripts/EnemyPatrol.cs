using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Knockback))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _sightRange = 5f;
    [SerializeField] private float _stopDistance = 0.8f;
    [SerializeField] private PlayerDeath _playerDeath;
    [SerializeField] private Transform _player;
    
    private Rigidbody2D _rigidbody;
    private int _currentPointIndex = 0;
    private Knockback _knockback;
    private Coroutine _patrolCoroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _knockback = GetComponent<Knockback>();
    }
    private void OnEnable()
    {
        if (_patrolCoroutine != null)
        {
            StopCoroutine(_patrolCoroutine);
        }
        
        _patrolCoroutine = StartCoroutine(PatrolBehavior());
    }

    private void Start()
    {
        _playerDeath.OnPlayerDied += StopPatrol;
    }

    private void OnDestroy()
    {
        _playerDeath.OnPlayerDied -= StopPatrol; 
    }

    private void StopPatrol()
    {
        if (_patrolCoroutine != null)
        {
            StopCoroutine(_patrolCoroutine);
            _patrolCoroutine = null;
        }
        
        _rigidbody.linearVelocity = Vector2.zero;
    }

    private IEnumerator PatrolBehavior()
    {
        bool isMoving = true;
        
        while (isMoving)
        {
            if (_player != null && IsPlayerInSight())
            {
                ChasePlayer();
            }
            else
            {
                Patrol();
            }
            
            yield return null;
        }
    }
    
    private void Patrol()
    {
        if (_knockback.IsKnockback) return; 
        
        Transform targetPoint = _patrolPoints[_currentPointIndex];
        Vector2 direction = (targetPoint.position - transform.position).normalized;
        _rigidbody.linearVelocity = new Vector2(direction.x * _moveSpeed, _rigidbody.linearVelocity.y);
        float minDistance = 0.1f;
        
        if (Mathf.Abs(transform.position.x - targetPoint.position.x) < minDistance)
        {
            _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
        }
    }

    private void ChasePlayer()
    {
        if (_knockback.IsKnockback) return; 
        
        if (_player != null)
        {
            Vector2 direction = (_player.position - transform.position).normalized;
            _rigidbody.linearVelocity = new Vector2(direction.x * _moveSpeed, _rigidbody.linearVelocity.y);
            float distance = Vector2.Distance(transform.position, _player.position);

            if (distance > _stopDistance)
            {
                _rigidbody.linearVelocity = direction * _moveSpeed;
            }
            else
            {
                _rigidbody.linearVelocity = Vector2.zero;
            }
        }
    }

    private bool IsPlayerInSight()
    {
        if (_player == null) return false;

        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
        return distanceToPlayer <= _sightRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
    }
}
