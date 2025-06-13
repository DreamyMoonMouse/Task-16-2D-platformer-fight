using UnityEngine;

public class ChaseBehavior : MonoBehaviour, IMobBehavior
{
    [SerializeField] private Mover _mover;
    [SerializeField] private float _stopDistance = 0.8f;
    
    private ITargetable _target;
    private float _stopDistanceSquared;

    private void Awake()
    {
        _stopDistanceSquared = _stopDistance * _stopDistance;
    }
    
    public void SetTarget(ITargetable target)
    {
        _target = target;
    }
    
    public void Execute()
    {
        Chase();
    }

    private void Chase()
    {
        if (_target == null)
            return;
        
        Vector2 direction = _target.GetTransform().position - transform.position;
        float distanceSquared = direction.sqrMagnitude;

        if (distanceSquared > _stopDistanceSquared)
        {
            _mover.MoveTo(_target.GetTransform().position);
        }
        else
        {
            _mover.Stop();
        }
    }
}
