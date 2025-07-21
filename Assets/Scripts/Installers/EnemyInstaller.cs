using UnityEngine;
using Zenject;
using EnemyModule;

public sealed class EnemyInstaller : MonoInstaller
{
    [SerializeField] 
    private float _cellSize = 10f;
    
    [SerializeField]
    private EnemyView _enemyViewPrefab;

    public override void InstallBindings()
    {
        Container.Bind<EnemySpatialGrid>().AsSingle().WithArguments(_cellSize);
        
        Container.BindInstance(_enemyViewPrefab).WhenInjectedInto<EnemyFactory>();
        
        Container.BindFactory<EnemyConfig, EnemyController, EnemyFactory>()
            .FromFactory<EnemyFactory>();
    }
}