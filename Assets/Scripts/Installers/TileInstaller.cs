using TileModule;
using Zenject;

public class TileInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TileManager>().FromComponentInHierarchy().AsSingle();
        
    }
}