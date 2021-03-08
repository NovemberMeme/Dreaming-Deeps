using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public class LoopBuildingGhost : BuildingGhost2D
    {
        protected override void Start()
        {
            RefreshVisual();

            LoopBuildingSystem._Instance.OnSelectedChanged += Instance_OnSelectedChanged;
        }

        protected override void LateUpdate()
        {
            Vector3 targetPosition = LoopBuildingSystem._Instance.GetMouseWorldSnappedPosition();
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

            transform.rotation = Quaternion.Lerp(transform.rotation, LoopBuildingSystem._Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
        }

        protected override void RefreshVisual()
        {
            if (visual != null)
            {
                Destroy(visual.gameObject);
                visual = null;
            }

            PlacedObjectTypeSO placedObjectTypeSO = LoopBuildingSystem._Instance.GetPlacedObjectTypeSO();

            if (placedObjectTypeSO != null)
            {
                visual = Instantiate(placedObjectTypeSO.visual, Vector3.zero, Quaternion.identity);
                visual.parent = transform;
                visual.localPosition = Vector3.zero;
                visual.localEulerAngles = Vector3.zero;
            }
        }
    }
}