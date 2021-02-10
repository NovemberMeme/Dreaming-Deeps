using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiegeTheSky
{
    public class MarkerClickResponse : MonoBehaviour, IClickable
    {
        [SerializeField] private GameObject _marker;

        private void Start()
        {
            DelegateManager.marker = _marker;
        }

        public void Click()
        {

        }

        public void Click(Vector3 newMarkerPosition)
        {
            _marker.transform.position = newMarkerPosition;
        }

        public void UnClickSelect()
        {

        }
    }
}