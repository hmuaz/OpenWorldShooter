using EnemyModule;
using Zenject;

namespace EnemyModule
{
    public class EnemyFactory : PlaceholderFactory<EnemyConfig, EnemyController>
    {
        private readonly DiContainer _container;
        
        private readonly EnemyView _enemyViewPrefab;
        
        private readonly EnemySpatialGrid _enemyGrid;
        
        private readonly TickableManager _tickableManager;

        public EnemyFactory(
            DiContainer container,
            EnemyView enemyViewPrefab,
            EnemySpatialGrid enemyGrid,
            TickableManager tickableManager)
        {
            _container = container;
            _enemyViewPrefab = enemyViewPrefab;
            _enemyGrid = enemyGrid;
            _tickableManager = tickableManager;
        }

        public override EnemyController Create(EnemyConfig config)
        {
            EnemyModel model = new EnemyModel(config);
            EnemyView view = _container.InstantiatePrefabForComponent<EnemyView>(_enemyViewPrefab);
            EnemyController controller = new EnemyController(model, view, _enemyGrid);

            _tickableManager.Add(controller);

            return controller;
        }
    }
}
