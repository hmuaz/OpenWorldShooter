using PlayerModule;
using TileModule;
using UnityEngine;
using Zenject;

public sealed class TileInstaller : MonoInstaller
{
    [SerializeField] 
    private GameObject _tilePrefab;
    
    [SerializeField] 
    private int _radius = 3;
    
    [SerializeField] 
    private float _tileSize = 10f;
    
    [SerializeField]
    private Transform _player;
    
    [SerializeField] 
    private Transform _tileParent;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TileController>().AsSingle() .WithArguments(_tilePrefab, _player, 
            _radius, _tileSize, _tileParent);

    }
}
