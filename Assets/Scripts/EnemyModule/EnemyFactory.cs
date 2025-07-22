using UnityEngine;
using Zenject;

namespace EnemyModule
{
    public class EnemyFactory
    {
        private readonly DiContainer _container;
        
        private readonly EnemyView _enemyViewPrefab;

        public EnemyFactory(DiContainer container, EnemyView enemyViewPrefab)
        {
            _container = container;
            _enemyViewPrefab = enemyViewPrefab;
        }

        public EnemyEntity CreateEnemy(EnemyConfig config, Vector3 spawnPosition, EnemySpatialGrid grid)
        {
            EnemyView view = _container.InstantiatePrefabForComponent<EnemyView>(_enemyViewPrefab);
            view.transform.position = spawnPosition;
            EnemyModel model = new EnemyModel(config);
            return new EnemyEntity(model, view, grid);
        }
    }
}