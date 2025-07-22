using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EnemyModule
{
    public sealed class EnemyController : ITickable
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly List<EnemyEntity> _enemies = new();
        private readonly EnemySpatialGrid _enemyGrid;

        public EnemyController(EnemyFactory enemyFactory, EnemySpatialGrid enemyGrid)
        {
            _enemyFactory = enemyFactory;
            _enemyGrid = enemyGrid;
        }

        public void AddEnemy(EnemyConfig config, Vector3 spawnPos)
        {
            Debug.Log("enemy added");
            var entity = _enemyFactory.CreateEnemy(config, spawnPos, _enemyGrid);
            _enemies.Add(entity);
        }

        public void Tick()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                EnemyEntity enemy = _enemies[i];
                if (enemy.Model.IsDead) continue;

                enemy.Tick();
            }
        }

        
    }
}
