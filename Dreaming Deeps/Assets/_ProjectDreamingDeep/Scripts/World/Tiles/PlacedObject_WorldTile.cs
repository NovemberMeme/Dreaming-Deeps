using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public class PlacedObject_WorldTile : PlacedObject_Done
    {
        [SerializeField] protected SpriteRenderer VisualsRenderer;

        [SerializeField] protected List<TileAspect> myAspects = new List<TileAspect>();
        public List<TileAspect> MyAspects
        {
            get => myAspects;
            set
            {
                myAspects = value;
                UpdateMyVisuals();
            }
        }

        public static PlacedObject_WorldTile Create(List<TileAspect> _startingAspects, Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO.Dir dir, PlacedObjectTypeSO placedObjectTypeSO)
        {
            Transform placedObjectTransform = Instantiate(placedObjectTypeSO.prefab, worldPosition, Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0));

            PlacedObject_WorldTile placedObject = placedObjectTransform.GetComponent<PlacedObject_WorldTile>();
            placedObject.Setup(placedObjectTypeSO, origin, dir);
            placedObject.MyAspects = _startingAspects;

            return placedObject;
        }

        protected virtual void UpdateMyVisuals()
        {
            if (myAspects.Count > 0)
                VisualsRenderer.sprite = myAspects[0].VisualsSprite;
        }
    }
}