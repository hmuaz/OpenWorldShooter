using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TileModule
{
    public sealed class TileController : ITickable
    {
        private readonly GameObject _tilePrefab;
        
        private readonly int _radius;
        
        private readonly float _tileSize;
        
        private readonly Transform _parent;
        
        private readonly Dictionary<Vector2Int, TileView> _activeTiles = new();
        
        private Transform _player;
        
        private Vector2Int _lastPlayerTile  = new Vector2Int(int.MinValue, int.MinValue);
        
        private List<Vector2Int> _tileCoordinatesToRemoveList = new List<Vector2Int>();

        public TileController(GameObject tilePrefab, Transform player, int radius, float tileSize, Transform parent)
        {
            _tilePrefab = tilePrefab;
            _player = player;
            _radius = radius;
            _tileSize = tileSize;
            _parent = parent;
        }

        public void Tick()
        {
            if (_player == null)
            {
                return;
            }
            
            Vector2Int playerTile = WorldToTile(_player.position);
            
            if (playerTile != _lastPlayerTile)
            {
                _lastPlayerTile = playerTile;
                UpdateTiles();
            }
        }

        private void UpdateTiles()
        {
            Vector2Int playerTileCoordinates = WorldToTile(_player.position);
            HashSet<Vector2Int> neededTileCoordinatesSet = new HashSet<Vector2Int>();

            for (int deltaX = -_radius; deltaX <= _radius; deltaX++)
            {
                for (int deltaZ = -_radius; deltaZ <= _radius; deltaZ++)
                {
                    Vector2Int currentTileCoordinates = new Vector2Int(playerTileCoordinates.x + deltaX, playerTileCoordinates.y + deltaZ);
                    neededTileCoordinatesSet.Add(currentTileCoordinates);

                    if (!_activeTiles.ContainsKey(currentTileCoordinates))
                    {
                        Vector3 currentTilePosition = new Vector3(currentTileCoordinates.x * _tileSize, 0f, currentTileCoordinates.y * _tileSize);
                        GameObject tileGameObject = Object.Instantiate(_tilePrefab, currentTilePosition, Quaternion.Euler(90, 0, 0), _parent);
                        TileModel tileModel = new TileModel(currentTileCoordinates, _tileSize);
                        TileView tileView = tileGameObject.GetComponent<TileView>();
                        tileView.SetModel(tileModel);
                        _activeTiles.Add(currentTileCoordinates, tileView);
                    }
                }
            }

            foreach (KeyValuePair<Vector2Int, TileView> activeTilePair in _activeTiles)
            {
                if (!neededTileCoordinatesSet.Contains(activeTilePair.Key))
                {
                    Object.Destroy(activeTilePair.Value.gameObject);
                    _tileCoordinatesToRemoveList.Add(activeTilePair.Key);
                }
            }

            foreach (Vector2Int tileCoordinatesToRemove in _tileCoordinatesToRemoveList)
            {
                _activeTiles.Remove(tileCoordinatesToRemove);
            }
        }

        private Vector2Int WorldToTile(Vector3 position)
        {
            int x = Mathf.FloorToInt(position.x / _tileSize);
            int z = Mathf.FloorToInt(position.z / _tileSize);
            return new Vector2Int(x, z);
        }

        public void SetPlayer(Transform player)
        {
            _player = player;
        }
    }
}