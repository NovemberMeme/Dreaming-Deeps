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

        public bool IsSubEffect;

        public AbilityData(PartyCharacter _user, PartyCharacter _target, float _physicaldmg, float _magicaldmg, bool _isSubEffect)
        {
            User = _user;
            Target = _target;
            PhysicalDamageAmount = _physicaldmg;
            MagicalDamageAmount = _magicaldmg;
            IsSubEffect = _isSubEffect;
        }
    }
}