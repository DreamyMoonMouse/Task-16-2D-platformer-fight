using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private CoinsPool _coinsPool;
    [SerializeField] private Canvas _winCanvas;
    [SerializeField] private Canvas _loseCanvas;
    [SerializeField] private Text _scoreText;

    private void Awake()
    {
        _winCanvas.gameObject.SetActive(false);
        _loseCanvas.gameObject.SetActive(false);
        _coinsPool.OnAllCoinsCollected += ShowWinMessage;
        _coinsPool.OnScoreUpdated += UpdateScore;
    }
    
    private void OnDestroy()
    {
        _coinsPool.OnAllCoinsCollected -= ShowWinMessage;
        _coinsPool.OnScoreUpdated -= UpdateScore; 
    }

    private void ShowWinMessage()
    {
        _winCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    
    private void ShowLoseMessage()
    {
        _loseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f; 
    }
    
    private void UpdateScore()
    {
        _scoreText.text = $"Score: {_coinsPool.CollectedCoins}";
    }
}
