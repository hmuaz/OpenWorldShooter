using System.Collections.Generic;
using UnityEngine;

public sealed class EnemySpatialGrid
{
    private readonly float _cellSize;

    private readonly Dictionary<Vector2Int, HashSet<Enemy>> _enemyGrid = new();

    public EnemySpatialGrid(float cellSize)
    {
        _cellSize = cellSize;
    }

    public void AddEnemy(Enemy enemy)
    {
        Vector2Int cell = GetCell(enemy.transform.position);
        if (!_enemyGrid.TryGetValue(cell, out HashSet<Enemy> set))
        {
            set = new HashSet<Enemy>();
            _enemyGrid[cell] = set;
        }
        set.Add(enemy);
        enemy.CurrentGridCell = cell;
    }

    public void RemoveEnemy(Enemy enemy, Vector2Int cell)
    {
        if (_enemyGrid.TryGetValue(cell, out HashSet<Enemy> set))
        {
            set.Remove(enemy);
            if (set.Count == 0)
            {
                _enemyGrid.Remove(cell);
            }
        }
    }

    public void UpdateEnemyCell(Enemy enemy)
    {
        Vector2Int newCell = GetCell(enemy.transform.position);
        if (enemy.CurrentGridCell != newCell)
        {
            RemoveEnemy(enemy, enemy.CurrentGridCell);
            AddEnemy(enemy);
        }
    }

    public void GetEnemiesInArea(Vector3 center, float areaSize, List<Enemy> resultList)
    {
        int half = Mathf.CeilToInt(areaSize / (2f * _cellSize));
        Vector2Int centerCell = GetCell(center);

        for (int dx = -half; dx <= half; dx++)
        {
            for (int dz = -half; dz <= half; dz++)
            {
                Vector2Int cell = new Vector2Int(centerCell.x + dx, centerCell.y + dz);
                if (_enemyGrid.TryGetValue(cell, out HashSet<Enemy> set))
                {
                    foreach (Enemy enemy in set)
                    {
                        resultList.Add(enemy);
                    }
                }
            }
        }
    }

    private Vector2Int GetCell(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / _cellSize);
        int z = Mathf.FloorToInt(position.z / _cellSize);
        return new Vector2Int(x, z);
    }
}
