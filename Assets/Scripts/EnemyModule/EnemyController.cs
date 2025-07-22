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

        public void AddEnemy(EnemyConfig config, Vector3 spawnPosition)
        {
            EnemyEntity entity = _enemyFactory.CreateEnemy(config, spawnPosition, _enemyGrid);
            _enemies.Add(entity);
        }

        public void Tick()
        {
            for (int index = 0; index < _enemies.Count; index++)
            {
                EnemyEntity enemy = _enemies[index];
                if (enemy.Model.IsDead) continue;

                enemy.Tick();
            }
        }

        
    }
}
