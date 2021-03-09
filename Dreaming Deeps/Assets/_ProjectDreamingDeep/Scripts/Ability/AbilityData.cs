using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public class AbilityData
    {
        public int TargetCharIndex;

        public PartyCharacter User;
        public PartyCharacter Target;

        public float PhysicalDamageAmount;
        public float MagicalDamageAmount;
    }
}