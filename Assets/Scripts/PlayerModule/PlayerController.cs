using System.Collections.Generic;
using UnityEngine;
using Zenject;
using EnemyModule;
using OpenWorldGame.Camera;
using OpenWorldGame.Input;

namespace PlayerModule
{
    public sealed class PlayerController : ITickable
    {
        private readonly EnemySpatialGrid _enemyGrid;
        
        private readonly InputController _inputController;

        private readonly List<PlayerView> _players = new();
        
        private readonly List<EnemyView> _nearbyEnemies = new();
        
        [Inject]
        private CameraController _cameraController;

        public PlayerController(
            EnemySpatialGrid enemyGrid,
            InputController inputController)
        {
            _enemyGrid = enemyGrid;
            _inputController = inputController;
        }

        public void AddPlayer(PlayerView view)
        {
            _players.Add(view);
        }

        public void Tick()
        {
            for (int playerIndex = 0; playerIndex < _players.Count; playerIndex++)
            {
                PlayerView player = _players[playerIndex];

                HandleMouseLook(player, _inputController.LookInput);
                HandleMovement(player, _inputController.MoveInput);

                if (_inputController.ShootTriggered)
                {
                    Shoot(player);
                }
            }
        }

        private void HandleMouseLook(PlayerView player, Vector2 lookInput)
        {
            _cameraController.HandleLook(
                lookInput,
                player.transform,
                player.CameraPivot,
                player.Model.MouseSensitivity,
                player.Model.MinVerticalAngle,
                player.Model.MaxVerticalAngle
            );
        }

        private void HandleMovement(PlayerView player, Vector2 moveInput)
        {
            player.Move(moveInput, player.Model.MoveSpeed);
        }

        private void Shoot(PlayerView player)
        {
            _nearbyEnemies.Clear();
            _enemyGrid.GetEnemiesInArea(player.PlayerCamera.transform.position, player.Model.ShootCheckArea, _nearbyEnemies);

            Vector3 origin = player.PlayerCamera.transform.position;
            Vector3 direction = player.PlayerCamera.transform.forward;

            EnemyView closestEnemy = null;
            float closestDistance = player.Model.ShootDistance + 1f;

            for (int index = 0; index < _nearbyEnemies.Count; index++)
            {
                EnemyView enemy = _nearbyEnemies[index];

                Vector3 toEnemy = enemy.Position - origin;
                float projection = Vector3.Dot(toEnemy, direction.normalized);

                if (projection < 0f || projection > player.Model.ShootDistance)
                {
                    continue;
                }

                Vector3 closestPoint = origin + direction.normalized * projection;
                float distanceToLine = Vector3.Distance(enemy.Position, closestPoint);

                if (distanceToLine <= player.Model.HitRadius && projection < closestDistance)
                {
                    closestEnemy = enemy;
                    closestDistance = projection;
                }
            }

            Vector3 endPoint = origin + direction * player.Model.ShootDistance;
            Debug.DrawLine(origin, endPoint, Color.red, 0.5f);

            if (closestEnemy != null)
            {
                closestEnemy.OnHit(player.Model.Damage);
            }
        }
    }
}
