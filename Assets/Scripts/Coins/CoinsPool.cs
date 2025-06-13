using UnityEngine;
using System.Collections.Generic;
using System;

public class CoinsPool : MonoBehaviour
{
    [SerializeField] private Collector collector;
    
    private int _collectedCoins = 0;
    private List<Coin> _coins = new List<Coin>();
    
    public event Action AllCoinsCollected;
    public event Action ScoreUpdated;
    
    public int CollectedCoins => _collectedCoins;
    
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            var coin = child.GetComponent<Coin>();
            
            if (coin != null)
                _coins.Add(coin);
        }
        
        collector.ItemCollected += HandleCollected;
    }
    
    private void OnDestroy()
    {
        collector.ItemCollected -= HandleCollected;
    }

    private void HandleCollected(ICollectable item)
    {
        if (item is Coin)
        {
            _collectedCoins++;
            ScoreUpdated?.Invoke();
            
            if (_collectedCoins == _coins.Count)
                AllCoinsCollected?.Invoke();
        }
    }
}
