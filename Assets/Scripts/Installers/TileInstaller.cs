using TileModule;
using Zenject;

public sealed class TileInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TileManager>().FromComponentInHierarchy().AsSingle();
        
    }
}