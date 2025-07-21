using UnityEngine;
using Zenject;

namespace EnemyModule
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyConfig[] _enemyConfigs; 

        [SerializeField]
        private int _spawnCount = 10;

        [SerializeField]
        private Vector2 _spawnAreaMin = new Vector2(-20f, -20f);

        [SerializeField]
        private Vector2 _spawnAreaMax = new Vector2(20f, 20f);

        [Inject]
        private EnemyFactory _enemyFactory;

        private void Start()
        {
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            int enemyTypeCount = _enemyConfigs.Length;
            for (int index = 0; index < _spawnCount; index++)
            {
                EnemyConfig config = _enemyConfigs[Random.Range(0, enemyTypeCount)];

                Vector3 spawnPos = new Vector3(
                    Random.Range(_spawnAreaMin.x, _spawnAreaMax.x),
                    0f,
                    Random.Range(_spawnAreaMin.y, _spawnAreaMax.y)
                );

                EnemyController enemy = _enemyFactory.Create(config);
                enemy.SetPosition(spawnPos);
            }
        }
    }
}

