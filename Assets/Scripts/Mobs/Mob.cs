using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Mover), typeof(Knockback))]
public class Mob : MonoBehaviour, ITargetable 
{
    [SerializeField] private PatrolBehavior _patrolBehavior;
    [SerializeField] private ChaseBehavior _chaseBehavior;
    [SerializeField] private PlayerDeath _playerDeath;
    [SerializeField] private Mover _mover;
    [SerializeField] private CharacterDetector _characterDetector;
    
    private Knockback _knockback;
    private Coroutine _behaviorCoroutine;

    private void Awake()
    {
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
        _playerDeath.PlayerDied += StopBehavior;
    }

    private void OnDestroy()
    {
        _playerDeath.PlayerDied -= StopBehavior;
    }
    
    public Transform GetTransform() 
        => transform;
    
    public bool IsPlayer() 
        => false;

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

            ITargetable target = _characterDetector.DetectNearestTarget();

            if (target != null)
            {
                _chaseBehavior.SetTarget(target);
                _chaseBehavior.Execute();
            }
            else
            {
                _patrolBehavior.Execute();
            }

            yield return null;
        }
    }
}
