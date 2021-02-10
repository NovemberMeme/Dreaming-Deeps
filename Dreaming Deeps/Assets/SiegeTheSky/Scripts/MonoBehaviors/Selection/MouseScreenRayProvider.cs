using UnityEngine;
using System.Collections.Generic;

namespace SiegeTheSky
{
    public class MouseScreenRayProvider : MonoBehaviour, IRayProvider
    {
        public Ray CreateRay()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            return ray;
        }
}
}