using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [System.Serializable]
    public class WorldTileData 
    {
        public List<TileAspect> SavedAspects = new List<TileAspect>();

        public WorldTileData(List<TileAspect> _initialAspects)
        {
            SavedAspects = _initialAspects;
        }
    }
}