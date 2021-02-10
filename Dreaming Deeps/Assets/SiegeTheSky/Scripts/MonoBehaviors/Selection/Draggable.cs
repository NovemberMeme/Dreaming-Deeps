using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiegeTheSky
{
    public class Draggable : MonoBehaviour
    {
        [SerializeField] private float distance = 10;

        [SerializeField] private bool isDragging = false;

        [SerializeField] private float dragThreshold = 1;

        [SerializeField] private Vector3 mousePos;

        private Vector3 lastPosition;

        public bool IsDragging { get => isDragging; set => isDragging = value; }

        private void OnDrawGizmos()
        {
            dragThreshold = 1;
        }

        public void BeginDrag(Vector3 _mousePos)
        {
            mousePos = _mousePos;

            if(Vector3.Distance(transform.position, _mousePos) > dragThreshold)
            {
                isDragging = true;
            }
        }

        public bool Drop()
        {
            bool _isDragging = isDragging;

            isDragging = false;

            // Attach Module

            //ModuleDropResponse moduleDropResponse = GetComponent<ModuleDropResponse>();

            //if(moduleDropResponse != null)
            //{
            //    moduleDropResponse.Drop();
            //}

            return _isDragging;

            //if(IsDragging)
            //{
            //}
            //else
            //{
            //    IClickable clickable = GetComponent<IClickable>();

            //    if (clickable != null)
            //        clickable.Click();
            //}
        }

        private void Update()
        {
            if (isDragging)
            {
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                objPosition.z = 0;
                transform.position = objPosition;
            }
        }
    }
}