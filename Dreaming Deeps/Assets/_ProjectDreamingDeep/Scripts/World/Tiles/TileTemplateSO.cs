using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Tile Template SO", menuName = "World Tiles/New Tile Template SO")]
    public class TileTemplateSO : ScriptableObject
    {
        public List<Vector2Int> AffectedTileVectors = new List<Vector2Int>();
    }
}