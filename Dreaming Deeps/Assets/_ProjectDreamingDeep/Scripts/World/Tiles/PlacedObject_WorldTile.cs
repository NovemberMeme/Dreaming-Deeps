using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public class PlacedObject_WorldTile : PlacedObject_Done
    {
        [SerializeField] protected PlacedWorldTileTypeSO placedWorldTileTypeSO;

        [SerializeField] protected GameObject DefaultVisuals;

        [SerializeField] protected List<TileAspect> myAspects = new List<TileAspect>();

        public List<TileAspect> MyAspects { get => myAspects; set => myAspects = value; }

        public static PlacedObject_WorldTile Create(Transform _worldTileParent, List<TileAspect> _startingAspects, Vector3 worldPosition, Vector2Int origin, PlacedWorldTileTypeSO.Dir dir, PlacedWorldTileTypeSO _placedWorldTileTypeSO)
        {
            Transform placedObjectTransform = Instantiate(_placedWorldTileTypeSO.prefab, worldPosition, Quaternion.Euler(0, _placedWorldTileTypeSO.GetRotationAngle(dir), 0), _worldTileParent);

            PlacedObject_WorldTile placedObject = placedObjectTransform.GetComponent<PlacedObject_WorldTile>();
            placedObject.SetupTile(_placedWorldTileTypeSO, origin, dir);
            placedObject.SetTileAspects(_startingAspects);

            return placedObject;
        }

        protected virtual void SetupTile(PlacedWorldTileTypeSO _placedWorldTileTypeSO, Vector2Int origin, PlacedObjectTypeSO.Dir dir)
        {
            this.placedWorldTileTypeSO = _placedWorldTileTypeSO;
            this.origin = origin;
            this.dir = dir;
        }

        public override List<Vector2Int> GetGridPositionList()
        {
            return placedWorldTileTypeSO.GetGridPositionList(origin, dir);
        }

        public virtual void DeleteAspects()
        {
            myAspects.Clear();

            UpdateMyVisuals();
        }

        public virtual void SetTileAspects(List<TileAspect> _newAspects)
        {
            myAspects = _newAspects;
            //for (int i = 0; i < _newAspects.Count; i++)
            //{
            //    if(!myAspects.Contains(_newAspects[i]))
            //        myAspects.Add(_newAspects[i]);
            //}

            UpdateMyVisuals();
        }

        public virtual void SetTileAspects(TileAspect _newAspect)
        {
            if (!myAspects.Contains(_newAspect))
                myAspects.Add(_newAspect);

            UpdateMyVisuals();
        }

        protected virtual void UpdateMyVisuals()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            if (MyAspects.Count < 1)
            {
                GameObject newVisuals = Instantiate(DefaultVisuals, transform.position, Quaternion.identity, transform);
            }
            else
            {
                GameObject newVisuals = Instantiate(MyAspects[0].VisualsPrefab, transform.position, Quaternion.identity, transform);
            }
        }
    }
}