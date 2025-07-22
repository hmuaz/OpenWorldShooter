using System.Collections.Generic;
using UnityEngine;

namespace EnemyModule
{
    public class EnemyEntity
    {
        private Vector3 _targetPosition;
        
        private float _fireTimer;
        
        private readonly EnemySpatialGrid _grid;

        public EnemyModel Model
        {
            get;
        }

        public EnemyView View
        {
            get;
        }

        public Vector2Int CurrentGridCell
        {
            get; private set;
        }
        public Vector3 Position => View.transform.position;
        public bool IsDead => Model.IsDead;

        public void SetGridCell(Vector2Int cell) => CurrentGridCell = cell;
        
        public void SetPosition(Vector3 position) => View.transform.position = position;

        public EnemyEntity(EnemyModel model, EnemyView view, EnemySpatialGrid grid)
        {
            Model = model;
            View = view;
            _grid = grid;

            _grid.AddEnemy(this);
            PickNewTarget();
        }

        public void Tick()
        {
            if (IsDead)
            {
                return;
            }
            
            _grid.UpdateEnemyCell(this);

            float distance = Vector3.Distance(Position, _targetPosition);
            if (distance < Model.ChangeTargetDistance)
            {
                PickNewTarget();
            }
            else
            {
                View.transform.position += (_targetPosition - Position).normalized * Model.MoveSpeed * Time.deltaTime;
            }

            _fireTimer -= Time.deltaTime;
            if (_fireTimer <= 0f)
            {
                TryShootOtherEnemy();
                _fireTimer = Model.FireCooldown;
            }
        }


        private void PickNewTarget()
        {
            Vector2 randomCircle = Random.insideUnitCircle * Model.WanderRadius;
            _targetPosition = new Vector3(randomCircle.x, Position.y, randomCircle.y);
        }

        public void OnHit(int damage)
        {
            if (IsDead)
            {
                return;
            }
            
            View.PlayHitEffect();
            Model.Health -= damage;

            if (Model.Health <= 0)
            {
                Die();
            }
                
        }

        private void Die()
        {
            if (IsDead)
            {
                return;
            }
            Model.IsDead = true;
            _grid.RemoveEnemy(this, CurrentGridCell);
            Object.Destroy(View.gameObject);
        }
        
        private void TryShootOtherEnemy()
        {
            List<EnemyEntity> closeEnemies = new List<EnemyEntity>();
            _grid.GetEnemiesInArea(Position, Model.ShootArea, closeEnemies);

            for (int index = 0; index < closeEnemies.Count; index++)
            {
                var target = closeEnemies[index];
                if (target == this || target.IsDead)
                {
                    continue;
                }

                float dist = Vector3.Distance(Position, target.Position);
                if (dist <= Model.ShootRange)
                {
                    target.OnHit(Model.Damage);
                    break;
                }
            }
        }

        
    }
}
