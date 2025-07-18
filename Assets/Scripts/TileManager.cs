using System.Collections.Generic;
using UnityEngine;

public sealed class TileManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _tilePrefab;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private int _radius = 3;
    
    [SerializeField] 
    private float _tileSize = 10f;


    private readonly Dictionary<Vector2Int, GameObject> _activeTiles = new Dictionary<Vector2Int, GameObject>();
    
    private Vector2Int _lastPlayerTile;

    private void Start()
    {
        UpdateTiles();
    }

    private void Update()
    {
        Vector2Int playerTile = WorldToTile(_player.position);
        if (playerTile != _lastPlayerTile)
        {
            _lastPlayerTile = playerTile;
            UpdateTiles();
        }
    }

    private void UpdateTiles()
    {
        Vector2Int playerTile = WorldToTile(_player.position);

        HashSet<Vector2Int> needed = new HashSet<Vector2Int>();
        for (int dx = -_radius; dx <= _radius; dx++)
        {
            for (int dz = -_radius; dz <= _radius; dz++)
            {
                Vector2Int tileCoord = new Vector2Int(playerTile.x + dx, playerTile.y + dz);
                needed.Add(tileCoord);
                if (!_activeTiles.ContainsKey(tileCoord))
                {
                    Vector3 position = new Vector3(tileCoord.x * _tileSize, 0f, tileCoord.y * _tileSize); //tileCoord.y çünkü quad kullanıyoruz.
                    GameObject tile = Instantiate(_tilePrefab, position, Quaternion.Euler(90f, 0f, 0f), transform); //x 90f çünkü quad dik geliyor.
                    _activeTiles.Add(tileCoord, tile);
                }
            }
        }

        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (KeyValuePair<Vector2Int, GameObject> keyValuePair in _activeTiles)
        {
            if (!needed.Contains(keyValuePair.Key))
            {
                Destroy(keyValuePair.Value);
                toRemove.Add(keyValuePair.Key);
            }
        }
        for (int index = 0; index < toRemove.Count; index++)
        {
            _activeTiles.Remove(toRemove[index]);
        }
    }

    private Vector2Int WorldToTile(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / _tileSize);
        int z = Mathf.FloorToInt(position.z / _tileSize);
        return new Vector2Int(x, z);
    }
}
