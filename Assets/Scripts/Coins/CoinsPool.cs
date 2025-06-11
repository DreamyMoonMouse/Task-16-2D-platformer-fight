using UnityEngine;
using System.Collections.Generic;
using System;

public class CoinsPool : MonoBehaviour
{
    [SerializeField] private Collector collector;
    
    private int _collectedCoins = 0;
    private List<Coin> _coins = new List<Coin>();
    
    public event Action OnAllCoinsCollected;
    public event Action OnScoreUpdated;
    
    public int CollectedCoins => _collectedCoins;
    
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            var coin = child.GetComponent<Coin>();
            
            if (coin != null)
                _coins.Add(coin);
        }
        
        collector.OnItemCollected += HandleCollected;
    }
    
    private void OnDestroy()
    {
        collector.OnItemCollected -= HandleCollected;
    }

    private void HandleCollected(ICollectable item)
    {
        if (item is Coin)
        {
            _collectedCoins++;
            OnScoreUpdated?.Invoke();
            
            if (_collectedCoins == _coins.Count)
                OnAllCoinsCollected?.Invoke();
        }
    }
}
