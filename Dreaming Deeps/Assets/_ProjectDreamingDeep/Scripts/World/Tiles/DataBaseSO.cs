using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Database SO", menuName = "SO/New Database SO")]
    public class DataBaseSO : ScriptableObject
    {
        public int GridWidth = 20;
        public int GridHeight = 15;

        public WorldTileData[,] WorldTileDatas;

        public void SaveWorldData(WorldTileData[,] _worldTileDatas)
        {
            WorldTileDatas = _worldTileDatas;
        }
    }
}