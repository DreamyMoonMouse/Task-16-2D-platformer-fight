using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class Collector : MonoBehaviour
{
    public event Action<ICollectable> OnItemCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ICollectable>(out ICollectable collectable))
        {
            collectable.Collect();
            OnItemCollected?.Invoke(collectable);
        }
    }
}


