using UnityEngine;
using Zenject;

namespace PlayerModule
{
    public class PlayerFactory
    {
        private readonly DiContainer _container;
        
        private readonly PlayerView _playerViewPrefab;
        
        public PlayerFactory(DiContainer container, PlayerView playerViewPrefab)
        {
            _container = container;
            _playerViewPrefab = playerViewPrefab;
        }

        public PlayerView CreatePlayer(Vector3 spawnPosition, PlayerConfig config)
        {
            PlayerView view = _container.InstantiatePrefabForComponent<PlayerView>(_playerViewPrefab);
            view.transform.position = spawnPosition;
            
            var model = new PlayerModel(
                config.maxHealth,
                config.damage,
                config.moveSpeed,
                config.mouseSensitivity,
                config.shootDistance,
                config.hitRadius,
                config.shootCheckArea,
                config.minVerticalAngle,
                config.maxVerticalAngle
            );
            
            view.SetModel(model);
            
            return view;
        }
    }

}

