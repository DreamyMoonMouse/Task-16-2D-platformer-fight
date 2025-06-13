using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class Collector : MonoBehaviour
{
    public event Action<ICollectable> ItemCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ICollectable>(out ICollectable collectable))
        {
            collectable.Collect();
            ItemCollected?.Invoke(collectable);
        }
    }
}


