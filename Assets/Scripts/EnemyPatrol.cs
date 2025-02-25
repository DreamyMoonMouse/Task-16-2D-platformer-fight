using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _moveSpeed = 3f;
    
    private int _currentPointIndex = 0;

    private void Start()
    {
        StartCoroutine(Patrol());
    }

    private IEnumerator Patrol()
    {
        bool isMoving = true;
        
        while (isMoving)
        {
            Transform targetPoint = _patrolPoints[_currentPointIndex];
            float minDistance = 0.1f;
            float minDistanceSqr = minDistance * minDistance;
            
            while ((transform.position - targetPoint.position).sqrMagnitude > minDistanceSqr)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, _moveSpeed * Time.deltaTime);
                yield return null; 
            }
            
            _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
        }
    }
}
