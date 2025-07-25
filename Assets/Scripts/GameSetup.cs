using UnityEngine;
using Zenject;
using PlayerModule;
using EnemyModule;
using TileModule;

namespace OpenWorldGame
{
    public sealed class GameSetup : MonoBehaviour
    {
        [Inject] 
        private PlayerFactory _playerFactory;
        
        [Inject] 
        private PlayerController _playerController;

        [Inject] 
        private EnemyFactory _enemyFactory;
        
        [Inject] 
        private EnemyController _enemyController;
        
        [Inject] 
        private TileController _tileController;

        [SerializeField] 
        private Vector3 _playerSpawnPosition = Vector3.zero;

        [SerializeField] 
        private PlayerConfig _playerConfig;

        [Header("Enemy Settings")]
        [SerializeField] 
        private EnemyConfig[] _enemyConfigs;
        
        [SerializeField] 
        private int _enemyCount = 10;
        
        [SerializeField] 
        private Vector2 _enemyAreaMin = new Vector2(-20, -20);
        [SerializeField] 
        private Vector2 _enemyAreaMax = new Vector2(20, 20);

        private void Start()
        {
            // --- PLAYER ---
            PlayerView view = _playerFactory.CreatePlayer(_playerSpawnPosition, _playerConfig);
            _tileController.SetPlayer(view.transform);
            _playerController.AddPlayer(view);

            // --- ENEMIES ---
            int enemyTypeCount = _enemyConfigs.Length;
            for (int i = 0; i < _enemyCount; i++)
            {
                EnemyConfig config = _enemyConfigs[Random.Range(0, enemyTypeCount)];
                Vector3 spawnPosition = new Vector3(
                    Random.Range(_enemyAreaMin.x, _enemyAreaMax.x),
                    0f,
                    Random.Range(_enemyAreaMin.y, _enemyAreaMax.y)
                );
                _enemyController.AddEnemy(config, spawnPosition);
            }
        }
    }
}