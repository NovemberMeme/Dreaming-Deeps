using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New World Tile SO", menuName = "World Tiles/New World Tile SO")]
    public class PlacedWorldTileTypeSO : PlacedObjectTypeSO
    {
        public TileTemplateSO UnOrientedGridPositionList;

        public override int GetRotationAngle(Dir dir)
        {
            switch (dir)
            {
                default:
                case Dir.Down: return 0;
                case Dir.Left: return 90;
                case Dir.Up: return 180;
                case Dir.Right: return 270;
            }
        }

        public virtual Vector2Int RotateVector2(Vector2Int _vector, Dir _dir)
        {
            switch (_dir)
            {
                default:
                case Dir.Down:
                    return _vector;
                case Dir.Left:
                    return new Vector2Int(_vector.y, -_vector.x);
                case Dir.Up:
                    return new Vector2Int(-_vector.x, -_vector.y);
                case Dir.Right:
                    return new Vector2Int(-_vector.y, _vector.x);
            }
        }

        public override List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir)
        {
            List<Vector2Int> gridPositionList = new List<Vector2Int>();

            for (int i = 0; i < UnOrientedGridPositionList.AffectedTileVectors.Count; i++)
            {
                gridPositionList.Add(offset + RotateVector2(UnOrientedGridPositionList.AffectedTileVectors[i], dir));
            }

            return gridPositionList;
        }
    }
}