using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EnemyModule
{
    public sealed class EnemyController: ITickable
    {
        [Inject]
        private SignalCenter _signalCenter;
        
        private readonly EnemyFactory _enemyFactory;
        
        private readonly List<EnemyView> _enemies = new();
        
        private readonly EnemySpatialGrid _enemyGrid;
        
        public EnemyController(EnemyFactory enemyFactory, EnemySpatialGrid enemyGrid, SignalCenter signalCenter)
        {
            _enemyFactory = enemyFactory;
            _enemyGrid = enemyGrid;
            _signalCenter = signalCenter;
            
            _signalCenter.Subscribe<EnemyDiedSignal>(OnEnemyDied);
        }

        public void AddEnemy(EnemyConfig config, Vector3 spawnPosition)
        {
            EnemyView view = _enemyFactory.CreateEnemy(config, spawnPosition);
            _enemies.Add(view);
        }

        public void Tick()
        {
            for (int index = 0; index < _enemies.Count; index++)
            {
                EnemyView enemy = _enemies[index];
                if (enemy.Model.IsDead) continue;
                
                _enemyGrid.UpdateEnemyCell(enemy);
                
                EnemyMoves(enemy);
                
                EnemyShoots(enemy);

            }
        }

        public void EnemyMoves(EnemyView enemy)
        {
            float distance = Vector3.Distance(enemy.Position, enemy.TargetPosition);
            if (distance < enemy.Model.ChangeTargetDistance)
            {
                EnemyRandomlyPicksNewTarget(enemy);
            }
            else
            {
                enemy.transform.position += (enemy.TargetPosition - enemy.Position).normalized * enemy.Model.MoveSpeed * Time.deltaTime;
            }
        }
        
        private void EnemyRandomlyPicksNewTarget(EnemyView enemy)
        {
            Vector2 randomCircle = Random.insideUnitCircle * enemy.Model.WanderRadius;
            enemy.TargetPosition = new Vector3(randomCircle.x, enemy.Position.y, randomCircle.y);
        }
        
        private void TryShootOtherEnemy(EnemyView enemy)
        {
            List<EnemyView> closeEnemies = new List<EnemyView>();
            _enemyGrid.GetEnemiesInArea(enemy.Position, enemy.Model.ShootArea, closeEnemies);

            for (int index = 0; index < closeEnemies.Count; index++)
            {
                EnemyView target = closeEnemies[index];
                if (target == enemy || target.IsDead)
                {
                    continue;
                }

                float distance = Vector3.Distance(enemy.Position, target.Position);
                if (distance <= enemy.Model.ShootRange)
                {
                    target.OnHit(enemy.Model.Damage);
                    
                    break;
                }
            }
        }

        public void EnemyShoots(EnemyView enemy)
        {
            enemy.FireTimer -= Time.deltaTime;
            if (enemy.FireTimer <= 0f)
            {
                TryShootOtherEnemy(enemy);
                enemy.FireTimer = enemy.Model.FireCooldown;
            }
        }
        
        private void OnEnemyDied(EnemyDiedSignal signal)
        {
            _enemyGrid.RemoveEnemy(signal.EnemyView, signal.EnemyView.CurrentGridCell);

        }
    }
}
