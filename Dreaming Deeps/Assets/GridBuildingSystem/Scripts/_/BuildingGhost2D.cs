using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost2D : MonoBehaviour {

    protected Transform visual;
    protected PlacedObjectTypeSO placedObjectTypeSO;

    protected virtual void Start() {
        RefreshVisual();

        GridBuildingSystem2D.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
    }

    protected virtual void Instance_OnSelectedChanged(object sender, System.EventArgs e) {
        RefreshVisual();
    }

    protected virtual void LateUpdate() {
        Vector3 targetPosition = GridBuildingSystem2D.Instance.GetMouseWorldSnappedPosition();
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

        transform.rotation = Quaternion.Lerp(transform.rotation, GridBuildingSystem2D.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }

    protected virtual void RefreshVisual() {
        if (visual != null) {
            Destroy(visual.gameObject);
            visual = null;
        }

        PlacedObjectTypeSO placedObjectTypeSO = GridBuildingSystem2D.Instance.GetPlacedObjectTypeSO();

        if (placedObjectTypeSO != null) {
            visual = Instantiate(placedObjectTypeSO.visual, Vector3.zero, Quaternion.identity);
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
        }
    }

}
