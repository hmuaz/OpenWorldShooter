using UnityEngine;
using Zenject;
using EnemyModule;

public sealed class EnemyInstaller : MonoInstaller
{
    [SerializeField] 
    private float _cellSize = 10f;

    public override void InstallBindings()
    {
        Container.Bind<EnemySpatialGrid>().AsSingle().WithArguments(_cellSize);
    }
}