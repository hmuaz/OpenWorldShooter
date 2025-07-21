using OpenWorldGame.Input;
using UnityEngine;
using Zenject;
using PlayerModule;

public sealed class PlayerInstaller : MonoInstaller
{
    [SerializeField] 
    private PlayerView _playerViewPrefab;
    
    [SerializeField] 
    private PlayerConfig _playerConfig;
    public override void InstallBindings()
    {
        Container.Bind<InputController>().FromComponentInHierarchy().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();

        Container.Bind<PlayerFactory>().AsSingle()
            .WithArguments(_playerViewPrefab, _playerConfig);
    }
}