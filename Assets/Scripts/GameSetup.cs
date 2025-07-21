using UnityEngine;
using Zenject;
using PlayerModule;

namespace OpenWorldGame
{
    public sealed class GameSetup : MonoBehaviour
    {
        [Inject] 
        private PlayerFactory _playerFactory;
        
        [Inject] 
        private PlayerController _playerController;

        [SerializeField] 
        private Vector3 _playerSpawnPosition = Vector3.zero;

        private void Start()
        {
            var playerEntity = _playerFactory.CreatePlayer(_playerSpawnPosition);
            _playerController.AddPlayer(
                playerEntity.View,
                playerEntity.Model,
                playerEntity.CameraPivot,
                playerEntity.PlayerCamera
            );
        }
    }
}