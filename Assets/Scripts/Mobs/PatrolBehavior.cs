using UnityEngine;

public class PatrolBehavior : MonoBehaviour, IMobBehavior
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private Mover _mover;
    
    private int _currentPointIndex = 0;
    
    public void Execute()
    {
        Patrol();
    }

    private void Patrol()
    {
        Transform targetPoint = _patrolPoints[_currentPointIndex];
        _mover.MoveTo(targetPoint.position);
        float minDistance = 0.5f;
        
        if (Mathf.Abs(transform.position.x - targetPoint.position.x) < minDistance)
        {
            _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
        }
    }
}
