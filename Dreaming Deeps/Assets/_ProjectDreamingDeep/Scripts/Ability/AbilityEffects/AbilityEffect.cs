using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Ability Effect", menuName = "Abilities/New Ability Effect")]
    public class AbilityEffect : ScriptableObject
    {
        public AbilityData MyAbilityData;

        public List<AbilityEffect> ExtraEffects = new List<AbilityEffect>();

        public virtual void ApplyEffects(AbilityData _abilityData)
        {
            ApplyMyCustomEffect(_abilityData);

            for (int i = 0; i < ExtraEffects.Count; i++)
            {
                ExtraEffects[i].ApplyEffects(_abilityData);
            }
        }

        public virtual void ApplyMyCustomEffect(AbilityData _abilityData)
        {

        }
    }
}