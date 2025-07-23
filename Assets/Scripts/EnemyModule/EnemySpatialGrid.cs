using System.Collections.Generic;
using UnityEngine;
using Zenject;
using EnemyModule; 

public sealed class EnemySpatialGrid
{
    [Inject]
    private SignalCenter _signalCenter;
    
    private readonly float _cellSize;

    private readonly Dictionary<Vector2Int, HashSet<EnemyView>> _enemyGrid = new();

    public EnemySpatialGrid(float cellSize)
    {
        _cellSize = cellSize;
    }

    public void AddEnemy(EnemyView enemy)
    {
        Vector2Int cell = GetCell(enemy.Position);

        if (!_enemyGrid.TryGetValue(cell, out var set))
        {
            set = new HashSet<EnemyView>();
            _enemyGrid[cell] = set;
        }
        set.Add(enemy);
        _signalCenter.Fire(new EnemyAmountChangedSignal(GetEnemyCount()));
        enemy.SetGridCell(cell);
    }

    public void RemoveEnemy(EnemyView enemy, Vector2Int cell)
    {
        if (_enemyGrid.TryGetValue(cell, out var set))
        {
            set.Remove(enemy);
            _signalCenter.Fire(new EnemyAmountChangedSignal(GetEnemyCount()));
            if (set.Count == 0)
            {
                _enemyGrid.Remove(cell);
            }
        }
    }

    public void UpdateEnemyCell(EnemyView enemy)
    {
        Vector2Int newCell = GetCell(enemy.Position);

        if (enemy.CurrentGridCell != newCell)
        {
            RemoveEnemy(enemy, enemy.CurrentGridCell);
            AddEnemy(enemy);
        }
    }

    public void GetEnemiesInArea(Vector3 center, float areaSize, List<EnemyView> resultList)
    {
        int halfCellCount = Mathf.CeilToInt(areaSize / (2f * _cellSize));
        Vector2Int centerCell = GetCell(center);

        for (int deltaX = -halfCellCount; deltaX <= halfCellCount; deltaX++)
        {
            for (int deltaZ = -halfCellCount; deltaZ <= halfCellCount; deltaZ++)
            {
                Vector2Int cell = new Vector2Int(centerCell.x + deltaX, centerCell.y + deltaZ);
                if (_enemyGrid.TryGetValue(cell, out var enemySet))
                {
                    foreach (var enemy in enemySet)
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
    
    public int GetEnemyCount()
    {
        int count = 0;
        foreach (var set in _enemyGrid.Values)
        {
            count += set.Count;
        }
        return count;
    }
}
