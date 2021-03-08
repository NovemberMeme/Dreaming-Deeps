using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DreamingDeep
{
    public class Drag : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Transform pos1;
        public Transform pos2;

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log(eventData.selectedObject.name);
            eventData.selectedObject.transform.position = pos2.position;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eventData.selectedObject.transform.position = pos1.position;
        }
    }
}