using UnityEngine;

namespace TileModule
{
    [RequireComponent(typeof(TileView))]
    public sealed class TileController : MonoBehaviour
    {
        private TileModel _model;
        
        private TileView _view;

        public void Initialize(TileModel model)
        {
            _model = model;
            _view = GetComponent<TileView>();

            transform.position = new Vector3(
                _model.TileCoord.x * _model.TileSize,
                0f,
                _model.TileCoord.y * _model.TileSize
            );

            _view.SetDefaultColor();
        }

        public void HighlightTile()
        {
            _view.Highlight();
        }

        public void ResetTile()
        {
            _view.SetDefaultColor();
        }

        public void SetTileColor(Color color)
        {
            _view.SetCustomColor(color);
        }
    }
}