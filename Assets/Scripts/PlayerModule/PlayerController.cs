using System.Collections.Generic;
using UnityEngine;
using Zenject;
using EnemyModule;
using OpenWorldGame.Input;

namespace PlayerModule
{
    public sealed class PlayerController : ITickable
    {
        private readonly EnemySpatialGrid _enemyGrid;
        
        private readonly InputController _inputController;

        private readonly List<PlayerEntity> _players = new();
        
        private readonly List<EnemyController> _nearbyEnemies = new();

        public PlayerController(
            EnemySpatialGrid enemyGrid,
            InputController inputController)
        {
            _enemyGrid = enemyGrid;
            _inputController = inputController;
        }

        public void AddPlayer(PlayerView view, PlayerModel model, Transform cameraPivot, Camera playerCamera)
        {
            _players.Add(new PlayerEntity(view, model, cameraPivot, playerCamera));
        }

        public void Tick()
        {
            for (int playerIndex = 0; playerIndex < _players.Count; playerIndex++)
            {
                PlayerEntity player = _players[playerIndex];

                HandleMouseLook(player, _inputController.LookInput);
                HandleMovement(player, _inputController.MoveInput);

                if (_inputController.IsShooting)
                {
                    Shoot(player);
                }
            }
        }

        private void HandleMouseLook(PlayerEntity player, Vector2 lookInput)
        {
            player.View.Look(
                lookInput,
                ref player.xRotation,
                player.Model.MouseSensitivity,
                player.Model.MinVerticalAngle,
                player.Model.MaxVerticalAngle,
                player.CameraPivot
            );
        }

        private void HandleMovement(PlayerEntity player, Vector2 moveInput)
        {
            player.View.Move(moveInput, player.Model.MoveSpeed);
        }

        private void Shoot(PlayerEntity player)
        {
            _nearbyEnemies.Clear();
            _enemyGrid.GetEnemiesInArea(player.PlayerCamera.transform.position, player.Model.ShootCheckArea, _nearbyEnemies);

            Vector3 origin = player.PlayerCamera.transform.position;
            Vector3 direction = player.PlayerCamera.transform.forward;

            EnemyController closestEnemy = null;
            float closestDistance = player.Model.ShootDistance + 1f;

            for (int index = 0; index < _nearbyEnemies.Count; index++)
            {
                EnemyController enemy = _nearbyEnemies[index];

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

        public class PlayerEntity
        {
            public PlayerView View { get; }
            
            public PlayerModel Model { get; }
            
            public Transform CameraPivot { get; }
            
            public Camera PlayerCamera { get; }
            
            public float xRotation;


            public PlayerEntity(PlayerView view, PlayerModel model, Transform cameraPivot, Camera playerCamera)
            {
                View = view;
                Model = model;
                CameraPivot = cameraPivot;
                PlayerCamera = playerCamera;
                xRotation = 0f;
            }
        }
    }
}
