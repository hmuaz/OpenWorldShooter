using UnityEngine;

public sealed class Enemy : MonoBehaviour
{
    public Vector2Int CurrentGridCell;

    private EnemySpatialGrid _enemyGrid;

    [SerializeField]
    private float _moveSpeed = 2f;

    [SerializeField]
    private float _wanderRadius = 15f;

    private Vector3 _targetPosition;
    
    private float _changeTargetDistance = 1.5f;

    private void Start()
    {
        _enemyGrid = FindObjectOfType<EnemySpatialGrid>();
        _enemyGrid.AddEnemy(this);

        PickNewTarget();
    }

    private void Update()
    {
        _enemyGrid.UpdateEnemyCell(this);

        float distance = Vector3.Distance(transform.position, _targetPosition);
        if (distance < _changeTargetDistance)
        {
            PickNewTarget();
        }
        else
        {
            Vector3 direction = (_targetPosition - transform.position).normalized;
            transform.position += direction * _moveSpeed * Time.deltaTime;
        }
    }

    private void PickNewTarget()
    {
        Vector2 randCircle = Random.insideUnitCircle * _wanderRadius;
        _targetPosition = new Vector3(randCircle.x, transform.position.y, randCircle.y);
    }

    private void OnDestroy()
    {
        _enemyGrid.RemoveEnemy(this, CurrentGridCell);
    }

    public void OnHit()
    {
        Debug.Log("Düşmanı vurdun!");
        Destroy(gameObject);
    }
}