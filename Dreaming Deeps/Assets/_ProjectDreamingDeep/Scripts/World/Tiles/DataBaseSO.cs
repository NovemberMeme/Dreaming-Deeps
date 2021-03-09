using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Database SO", menuName = "SO/New Database SO")]
    public class DataBaseSO : ScriptableObject
    {
        public bool ShowDebug = false;

        public int GridWidth = 20;
        public int GridHeight = 15;

        public List<WorldTileData> WorldTileDatas = new List<WorldTileData>();

        public void SaveWorldData(List<WorldTileData> _worldTileDatas)
        {
            WorldTileDatas = _worldTileDatas;
        }
    }
}