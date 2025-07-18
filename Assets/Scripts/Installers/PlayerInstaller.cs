using UnityEngine;
using Zenject;
using PlayerModule;
public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
    }
}