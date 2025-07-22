using UnityEngine;

namespace TileModule
{
    public class TileEntity
    {
        public TileModel Model { get; }
        
        public TileView View { get; }
        
        public Vector2Int Coordination => Model.TileCoordination;

        public TileEntity(TileModel model, TileView view)
        {
            Model = model;
            View = view;
        }
    
        public void Highlight() => View.Highlight();
        public void Reset() => View.SetDefaultColor();
        public void SetColor(Color c) => View.SetCustomColor(c);
    }

}

