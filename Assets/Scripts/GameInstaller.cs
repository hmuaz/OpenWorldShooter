using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [SerializeField] 
    private EnemySpatialGrid _enemyGrid;
    
    [SerializeField] 
    private float _enemyCellSize = 10f;

    
    [SerializeField] 
    private TileManager _tileManager;

    public override void InstallBindings()
    {
        Container.Bind<EnemySpatialGrid>().AsSingle().WithArguments(_enemyCellSize);
        Container.Bind<TileManager>().FromInstance(_tileManager).AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Enemy>().FromComponentInHierarchy().AsTransient();
    }
}