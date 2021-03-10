using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public static class Utils 
    {
        public static float GetTicksPerSecond()
        {
            float ticksPerSecond = 60;

            if (DelegateController.getTicksPerSecond != null)
                ticksPerSecond = DelegateController.getTicksPerSecond.Invoke();

            return ticksPerSecond;
        }
    }
}