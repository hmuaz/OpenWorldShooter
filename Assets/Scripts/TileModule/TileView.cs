using UnityEngine;

namespace TileModule
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] 
        private Renderer _renderer;
        
        [SerializeField] 
        private Color _defaultColor = Color.grey;
        
        [SerializeField] 
        private Color _highlightColor = Color.yellow;

        private void Awake()
        {
            if (_renderer == null)
            {
                _renderer = GetComponentInChildren<Renderer>();
            }
            SetDefaultColor();
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