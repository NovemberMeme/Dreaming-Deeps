using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New TileAspect", menuName = "World Tiles/New TileAspect")]
    public class TileAspect : ScriptableObject
    {
        public GameObject VisualsPrefab;

        public bool IsLoopPathAspect = false;
    }
}