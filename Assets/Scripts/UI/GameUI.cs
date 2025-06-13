using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private CoinsPool _coinsPool;
    [SerializeField] private Canvas _winCanvas;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private Text _scoreText;
    [SerializeField] private PlayerDeath _playerDeath;

    private void Awake()
    {
        _winCanvas.gameObject.SetActive(false);
        _gameOverCanvas.gameObject.SetActive(false);
        _coinsPool.AllCoinsCollected += ShowWinMessage;
        _coinsPool.ScoreUpdated += UpdateScore;
        _playerDeath.PlayerDied += ShowGameOver;
    }
    
    private void OnDestroy()
    {
        _coinsPool.AllCoinsCollected -= ShowWinMessage;
        _coinsPool.ScoreUpdated -= UpdateScore; 
        _playerDeath.PlayerDied -= ShowGameOver;
    }

    private void ShowWinMessage()
    {
        _winCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    
    private void ShowGameOver()
    {
        _gameOverCanvas.gameObject.SetActive(true);
    }
    
    private void UpdateScore()
    {
        _scoreText.text = $"Score: {_coinsPool.CollectedCoins}";
    }
}
