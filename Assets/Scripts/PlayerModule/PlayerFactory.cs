using UnityEngine;
using Zenject;

namespace PlayerModule
{
    public class PlayerFactory
    {
        private readonly DiContainer _container;
        
        private readonly PlayerView _playerViewPrefab;
        
        private readonly PlayerConfig _playerConfig;

        public PlayerFactory(DiContainer container, PlayerView playerViewPrefab, PlayerConfig playerConfig)
        {
            _container = container;
            _playerViewPrefab = playerViewPrefab;
            _playerConfig = playerConfig;
        }

        public PlayerController.PlayerEntity CreatePlayer(Vector3 spawnPosition)
        {
            PlayerView view = _container.InstantiatePrefabForComponent<PlayerView>(_playerViewPrefab);
            view.transform.position = spawnPosition;
            
            Transform cameraPivot = view.transform.GetChild(0);
            
            Camera playerCamera = cameraPivot.GetChild(0).GetComponent<Camera>();
            
            PlayerModel model = new PlayerModel(_playerConfig);
            
            return new PlayerController.PlayerEntity(view, model, cameraPivot, playerCamera);
        }
    }

}

