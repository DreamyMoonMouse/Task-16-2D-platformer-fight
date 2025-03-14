using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class HealthPackCollector : MonoBehaviour
{
    public event Action<HealthPack> OnHealthPackCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HealthPack>(out HealthPack healthPack))
        {
            healthPack.Collect();
            OnHealthPackCollected?.Invoke(healthPack);
        }
    }
}
