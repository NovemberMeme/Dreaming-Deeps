using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Database SO", menuName = "SO/New Database SO")]
    public class DataBaseSO : ScriptableObject
    {
        [Header("Party Character Settings: ")]

        public float BaseAttackDuration = 0.4f;
        public float MaxAttackDuration = 0.8f;
        public float BaseAbilityDuration = 0.4f;
        public float MaxAbilityDuration = 1.2f;

        [Header("Loop Building System Settings: ")]

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