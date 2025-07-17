using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [SerializeField] 
    private EnemySpatialGrid _enemyGrid;
    
    [SerializeField] 
    private TileManager _tileManager;

    public override void InstallBindings()
    {
        Container.Bind<EnemySpatialGrid>().FromInstance(_enemyGrid).AsSingle();
        Container.Bind<TileManager>().FromInstance(_tileManager).AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Enemy>().FromComponentInHierarchy().AsTransient();
    }
}