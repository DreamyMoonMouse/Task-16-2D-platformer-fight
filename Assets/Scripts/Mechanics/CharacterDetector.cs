using UnityEngine;
using System.Collections.Generic;

public class CharacterDetector : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private LayerMask _targetLayerMask = ~0; 

    private float _detectionRangeSquared;

    private void Awake()
    {
        _detectionRangeSquared = _detectionRange * _detectionRange;
    }

    public ITargetable DetectNearestTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _detectionRange, _targetLayerMask);

        ITargetable nearestTarget = null;
        float minSqrDistance = float.MaxValue;

        foreach (var collider in targets)
        {
            if (collider.TryGetComponent(out ITargetable target))
            {
                Vector2 direction = target.GetTransform().position - transform.position;
                float distanceSquared = direction.sqrMagnitude;

                if (distanceSquared < minSqrDistance)
                {
                    minSqrDistance = distanceSquared;
                    nearestTarget = target;
                }
            }
        }

        return nearestTarget;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }
}
