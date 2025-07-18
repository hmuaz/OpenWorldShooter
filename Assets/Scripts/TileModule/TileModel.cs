using UnityEngine;

namespace TileModule
{
    public sealed class TileModel
    {
        private readonly Vector2Int _tileCoord;
        
        private readonly float _tileSize;

        public Vector2Int TileCoord => _tileCoord;
        
        public float TileSize => _tileSize;
        
        public TileModel(Vector2Int tileCoord, float tileSize)
        {
            _tileCoord = tileCoord;
            _tileSize = tileSize;
        }
    }
}