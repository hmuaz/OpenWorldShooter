using UnityEngine;

namespace TileModule
{
    public sealed class TileView : MonoBehaviour
    {
        [SerializeField] 
        private Renderer _renderer;
        
        [SerializeField] 
        private Color _defaultColor = Color.grey;
        
        [SerializeField] 
        private Color _highlightColor = Color.yellow;
        
        private TileModel _tileModel;

        private void Awake()
        {
            if (_renderer == null)
            {
                _renderer = GetComponentInChildren<Renderer>();
            }
            SetDefaultColor();
        }

        public void SetModel(TileModel tileModel)
        {
            _tileModel = tileModel;
        }

        public void SetDefaultColor()
        {
            if (_renderer != null)
            {
                _renderer.material.color = _defaultColor;
            }
        }

        public void Highlight()
        {
            if (_renderer != null)
            {
                _renderer.material.color = _highlightColor;
            }
        }

        public void SetCustomColor(Color color)
        {
            if (_renderer != null)
            {
                _renderer.material.color = color;
            }
        }
        
    }
}