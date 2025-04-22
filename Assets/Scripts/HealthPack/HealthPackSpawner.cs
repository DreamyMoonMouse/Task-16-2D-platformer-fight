using UnityEngine;

public class HealthPackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _healthPackPrefab;
    [SerializeField] private int _numberOfPacks = 3;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _spawnHeightOffset = 0.5f;
    [SerializeField] private Transform _healthPacksParent;

    private void Start()
    {
        for (int i = 0; i < _numberOfPacks; i++)
        {
            Vector2 spawnPosition = GetRandomGroundPosition();
            
            if (spawnPosition != Vector2.zero)
            {
                Instantiate(_healthPackPrefab, spawnPosition, Quaternion.identity, _healthPacksParent);
            }
        }
    }

    private Vector2 GetRandomGroundPosition()
    {
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(Vector2.zero, 1000f, _groundLayer);
        
        if (groundColliders.Length == 0) return Vector2.zero;
        
        Collider2D randomPlatform = groundColliders[Random.Range(0, groundColliders.Length)];
        Bounds bounds = randomPlatform.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float spawnY = bounds.max.y + _spawnHeightOffset;

        return new Vector2(randomX, spawnY);
    }
} 
