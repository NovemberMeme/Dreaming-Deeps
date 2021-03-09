using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public class Ability : MonoBehaviour
    {
        public AbilityType MyAbilityType;
        public float AbilityManacost = 5;

        public ITargetType MyTargetType;

        public AbilityEffect MyAbilityEffects;

        public virtual void ApplyAbility(AbilityData _abilityData)
        {
            MyAbilityEffects.ApplyEffects(_abilityData);
        }
    }
}