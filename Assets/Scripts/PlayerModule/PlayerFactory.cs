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

        public PlayerController.PlayerEntity CreatePlayer(Vector3 spawnPosition, PlayerConfig config)
        {
            PlayerView view = _container.InstantiatePrefabForComponent<PlayerView>(_playerViewPrefab);
            view.transform.position = spawnPosition;
            
            Transform cameraPivot = view.transform.GetChild(0);
            
            Camera playerCamera = cameraPivot.GetChild(0).GetComponent<Camera>();
            
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
            
            return new PlayerController.PlayerEntity(view, model, cameraPivot, playerCamera);
        }
    }

}

