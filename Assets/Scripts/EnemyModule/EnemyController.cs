using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EnemyModule
{
    [RequireComponent(typeof(EnemyView))]
    public class EnemyController : MonoBehaviour
    {
        [Inject] 
        private EnemySpatialGrid _enemyGrid;

        [SerializeField] 
        private EnemyType _type;

        private EnemyModel _model;
        
        private EnemyView _view;

        private Vector3 _targetPosition;
        
        private float _fireTimer = 0f;

        public Vector2Int CurrentGridCell
        {
            get; 
            private set;
        }

        private void Awake()
        {
            _view = GetComponent<EnemyView>();
            _model = new EnemyModel(_type);
        }

        private void Start()
        {
            _enemyGrid.AddEnemy(this);
            PickNewTarget();
        }

        private void Update()
        {
            if (_model.IsDead)
            {
                return;
            }

            _enemyGrid.UpdateEnemyCell(this);

            float distance = Vector3.Distance(transform.position, _targetPosition);
            if (distance < _model.ChangeTargetDistance)
            {
                PickNewTarget();
            }
            else
            {
                Vector3 direction = (_targetPosition - transform.position).normalized;
                transform.position += direction * _model.MoveSpeed * Time.deltaTime;
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
            _enemyGrid.GetEnemiesInArea(transform.position, _model.ShootArea, closeEnemies);

            for (int i = 0; i < closeEnemies.Count; i++)
            {
                EnemyController target = closeEnemies[i];

                if (target == this || target._model.IsDead)
                {
                    continue;
                }

                float dist = Vector3.Distance(transform.position, target.transform.position);
                if (dist <= _model.ShootRange)
                {
                    target.OnHit(_model.Damage);
                    Debug.Log($"{gameObject.name} diğer enemy'e ateş etti! ({target.name})");
                    break;
                }
            }
        }

        private void PickNewTarget()
        {
            Vector2 randCircle = Random.insideUnitCircle * _model.WanderRadius;
            _targetPosition = new Vector3(randCircle.x, transform.position.y, randCircle.y);
        }

        private void OnDestroy()
        {
            _enemyGrid.RemoveEnemy(this, CurrentGridCell);
        }

        public void OnHit(int damage)
        {
            if (_model.IsDead)
            {
                return;
            }

            _view.PlayHitEffect();

            _model.Health -= damage;
            Debug.Log($"Düşman hasar aldı: {damage}, kalan health: {_model.Health}");

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

            //_view.PlayDeathEffect();

            Debug.Log("Düşman öldü!");
            Destroy(gameObject);
        }

        public void SetGridCell(Vector2Int cell)
        {
            CurrentGridCell = cell;
        }
    }
}
