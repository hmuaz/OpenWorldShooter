using UnityEngine;

namespace TileModule
{
    public sealed class TileModel
    {
        private readonly Vector2Int _tileCoordination;
        
        private readonly float _tileSize;

        public Vector2Int TileCoordination => _tileCoordination;
        
        public float TileSize => _tileSize;
        
        public TileModel(Vector2Int tileCoordination, float tileSize)
        {
            _tileCoordination = tileCoordination;
            _tileSize = tileSize;
        }
    }
}