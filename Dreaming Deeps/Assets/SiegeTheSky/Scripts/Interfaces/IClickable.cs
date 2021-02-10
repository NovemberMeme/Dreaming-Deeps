using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiegeTheSky
{
    public interface IClickable
    {
        void Click();

        void Click(Vector3 pos);

        void UnClickSelect();
    }
}