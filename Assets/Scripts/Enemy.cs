using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class Enemy : MonoBehaviour
{
    [Inject] private EnemySpatialGrid _enemyGrid;

    [SerializeField] private EnemyType _type;

    public Vector2Int CurrentGridCell { get; set; }

    private int _health;
    private Vector3 _targetPosition;
    private float _changeTargetDistance = 1.5f;
    private bool _isDead = false;

    private float _fireCooldown = 1.0f;
    private float _fireTimer = 0f;
    private float _shootRange = 8f; 
    private float _shootArea = 12f; 

    public int Damage => _type.Damage;
    public int MaxHealth => _type.MaxHealth;

    private void Start()
    {
        _enemyGrid.AddEnemy(this);
        _health = _type.MaxHealth;
        PickNewTarget();
    }

    private void Update()
    {
        if (_isDead) { return; }

        _enemyGrid.UpdateEnemyCell(this);

        float distance = Vector3.Distance(transform.position, _targetPosition);
        if (distance < _changeTargetDistance)
        {
            PickNewTarget();
        }
        else
        {
            Vector3 direction = (_targetPosition - transform.position).normalized;
            transform.position += direction * _type.MoveSpeed * Time.deltaTime;
        }

        _fireTimer -= Time.deltaTime;
        if (_fireTimer <= 0f)
        {
            TryShootOtherEnemy();
            _fireTimer = _fireCooldown;
        }
    }

    private void TryShootOtherEnemy()
    {
        List<Enemy> closeEnemies = new List<Enemy>();
        _enemyGrid.GetEnemiesInArea(transform.position, _shootArea, closeEnemies);

        for (int i = 0; i < closeEnemies.Count; i++)
        {
            Enemy target = closeEnemies[i];

            if (target == this || target._isDead) { continue; }

            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist <= _shootRange)
            {
                target.OnHit(_type.Damage);
                Debug.Log($"{gameObject.name} diğer enemy'e ateş etti! ({target.name})");
                break; 
            }
        }
    }

    private void PickNewTarget()
    {
        Vector2 randCircle = Random.insideUnitCircle * _type.WanderRadius;
        _targetPosition = new Vector3(randCircle.x, transform.position.y, randCircle.y);
    }

    private void OnDestroy()
    {
        _enemyGrid.RemoveEnemy(this, CurrentGridCell);
    }

    public void OnHit(int damage)
    {
        if (_isDead) { return; }
        
        LeanTween.moveX(gameObject, transform.position.x + 0.2f, 0.1f).setLoopPingPong(1);

        _health -= damage;
        Debug.Log($"Düşman hasar aldı: {damage}, kalan health: {_health}");

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_isDead) { return; }
        _isDead = true;
        Debug.Log("Düşman öldü!");
        Destroy(gameObject);
    }
}
