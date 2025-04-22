using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class CoinCollector : MonoBehaviour
{
    public event Action<Coin> OnCoinCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
        {
            coin.Collect();
            OnCoinCollected?.Invoke(coin);
        }
    }
}
