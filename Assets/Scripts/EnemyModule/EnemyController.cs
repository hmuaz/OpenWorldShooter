using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EnemyModule
{
    
    public sealed class EnemyController : ITickable
    {
        private readonly EnemyModel _model;
        
        private readonly EnemyView _view;
        
        private readonly EnemySpatialGrid _enemyGrid;
        
        private Vector3 _targetPosition;
        
        private float _fireTimer;

        public Vector2Int CurrentGridCell
        {
            get; 
            private set;
        }
        
        public Vector3 Position
        {
            get
            {
                return _view.transform.position;
            }
        }
        
        public bool IsDead => _model.IsDead;

        public EnemyController(
            EnemyModel model,
            EnemyView view,
            EnemySpatialGrid enemyGrid)
        {
            _model = model;
            _view = view;
            _enemyGrid = enemyGrid;

            _enemyGrid.AddEnemy(this);
            PickNewTarget();
        }

        public void Tick()
        {
            if (_model.IsDead)
            {
                return;
            }

            _enemyGrid.UpdateEnemyCell(this);

            float distance = Vector3.Distance(_view.transform.position, _targetPosition);
            if (distance < _model.ChangeTargetDistance)
            {
                PickNewTarget();
            }
            else
            {
                Vector3 direction = (_targetPosition - _view.transform.position).normalized;
                _view.transform.position += direction * _model.MoveSpeed * Time.deltaTime;
            }

            _fireTimer -= Time.deltaTime;
            if (_fireTimer <= 0f)
            {
                TryShootOtherEnemy();
                _fireTimer = _model.FireCooldown;
            }
        }

        private void TryShootOtherEnemy()
        {
            List<EnemyController> closeEnemies = new List<EnemyController>();
            _enemyGrid.GetEnemiesInArea(_view.transform.position, _model.ShootArea, closeEnemies);

            for (int index = 0; index < closeEnemies.Count; index++)
            {
                EnemyController target = closeEnemies[index];
                if (target == this || target.IsDead)
                {
                    continue;
                }
                float dist = Vector3.Distance(_view.transform.position, target._view.transform.position);
                if (dist <= _model.ShootRange)
                {
                    target.OnHit(_model.Damage);
                    break;
                }
            }
        }

        private void PickNewTarget()
        {
            Vector2 randCircle = Random.insideUnitCircle * _model.WanderRadius;
            _targetPosition = new Vector3(randCircle.x, _view.transform.position.y, randCircle.y);
        }

        public void OnHit(int damage)
        {
            if (_model.IsDead)
            {
                return;
            }
            _view.PlayHitEffect();
            _model.Health -= damage;
            if (_model.Health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (_model.IsDead)
            {
                return;
            }
            _model.IsDead = true;
            
            _enemyGrid.RemoveEnemy(this, CurrentGridCell);

            Object.Destroy(_view.gameObject);
            //_view.PlayDeathEffect();
        }

        public void SetGridCell(Vector2Int cell)
        {
            CurrentGridCell = cell;
        }
        
        
        public void SetPosition(Vector3 position)
        {
            _view.transform.position = position;
        }
    }
}
