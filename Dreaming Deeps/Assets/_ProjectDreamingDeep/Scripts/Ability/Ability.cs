using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public enum TargetType
    {
        Frontmost,
        Backmost,
        LowestStat,
        HighestStat
    }

    public class Ability : MonoBehaviour
    {
        public AbilityType MyAbilityType;
        public float AbilityChargeDelay = 5;

        public TargetType MyTargetType = TargetType.Frontmost;
    }
}