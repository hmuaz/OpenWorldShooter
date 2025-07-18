using UnityEngine;
using Zenject;
using PlayerModule;
public sealed class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
    }
}