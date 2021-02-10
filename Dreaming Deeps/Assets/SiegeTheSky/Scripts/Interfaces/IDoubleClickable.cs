using UnityEngine;

namespace SiegeTheSky
{
    public interface IDoubleClickable
    {
        void DoubleClick();

        void DoubleClick(GameObject materialObject, Vector3 currentMarkerPosition);
    }
}