using UnityEngine;

public class CharacterDetector : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private bool _isMobDetector = true; 

    public ITargetable DetectNearestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _detectionRange);

        ITargetable nearestTarget = null;
        float minSqrDistance = float.MaxValue;

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out ITargetable target))
            {
                if (target.GetTransform() == transform)
                    continue;
                
                if (_isMobDetector && target.IsPlayer() == false)
                    continue;
                
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
