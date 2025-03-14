using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PlayerDeath _playerDeath;
    [SerializeField] private Transform _enemiesPool; 

    private List<EnemyData> _enemies = new List<EnemyData>();

    private void Awake()
    {
        foreach (Transform child in _enemiesPool)
        {
            GameObject enemy = child.gameObject;
            _enemies.Add(new EnemyData
            {
                EnemyObject = enemy,
                InitialPosition = enemy.transform.position
            });
        }

        _playerDeath.OnGameRestarted += RespawnEnemies;
    }

    private void OnDestroy()
    {
        _playerDeath.OnGameRestarted -= RespawnEnemies;
    }

    private void RespawnEnemies()
    {
        foreach (EnemyData enemyData in _enemies)
        {
            GameObject enemy = enemyData.EnemyObject;
            
            if (enemy.activeSelf == false)
            {
                enemy.GetComponent<MonoBehaviour>().StopAllCoroutines();
                enemy.SetActive(true);
                EnemyDeath enemyDeath = enemy.GetComponent<EnemyDeath>();
                
                if (enemyDeath != null)
                {
                    enemyDeath.GetComponent<Health>().Heal(enemyDeath.GetComponent<Health>().MaxHealth);
                }
                
                enemy.transform.position = enemyData.InitialPosition;
            }
        }
    }

    [System.Serializable]
    private class EnemyData
    {
        public GameObject EnemyObject;
        [HideInInspector] public Vector3 InitialPosition;
    }
}
