using Zenject;

public sealed class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SignalCenter>().AsSingle();
    }
}