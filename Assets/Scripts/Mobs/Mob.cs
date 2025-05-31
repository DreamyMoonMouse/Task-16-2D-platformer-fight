using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Mover), typeof(Knockback))]
public class Mob : MonoBehaviour
{
    [SerializeField] private PatrolBehavior _patrolBehavior;
    [SerializeField] private ChaseBehavior _chaseBehavior;
    [SerializeField] private float _sightRange = 5f;
    [SerializeField] private PlayerDeath _playerDeath;
    [SerializeField] private Transform _player;
    
    private Mover _mover;
    private Knockback _knockback;
    private Coroutine _behaviorCoroutine;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _knockback = GetComponent<Knockback>();
    }

    private void OnEnable()
    {
        if (_behaviorCoroutine != null) 
            StopCoroutine(_behaviorCoroutine);
        
        _behaviorCoroutine = StartCoroutine(BehaviorRoutine());
    }

    private void Start()
    {
        _playerDeath.OnPlayerDied += StopBehavior;
    }

    private void OnDestroy()
    {
        _playerDeath.OnPlayerDied -= StopBehavior;
    }

    private void StopBehavior()
    {
        if (_behaviorCoroutine != null)
        {
            StopCoroutine(_behaviorCoroutine);
            _behaviorCoroutine = null;
        }
        
        _mover.Stop();
    }

    private IEnumerator BehaviorRoutine()
    {
        bool isRunning = true;
        
        while (isRunning)
        {
            if (_knockback.IsKnockback)
            {
                yield return null;
                continue;
            }
            
            IMobBehavior currentBehavior = (_player != null && IsPlayerInSight()) ? _chaseBehavior : _patrolBehavior;
            currentBehavior.Execute();
            
            yield return null;
        }
    }

    private bool IsPlayerInSight()
    {
        if (_player == null) 
            return false;
        
        return Vector2.Distance(transform.position, _player.position) <= _sightRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
    }
}
