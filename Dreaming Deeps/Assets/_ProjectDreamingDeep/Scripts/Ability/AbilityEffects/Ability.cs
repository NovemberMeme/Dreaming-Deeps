using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Ability Effect", menuName = "Abilities/New Ability Effect")]
    public class Ability : ScriptableObject
    {
        public ITargetType MyTargetType;

        public bool IsSubEffect = false;

        public List<Ability> ExtraEffects = new List<Ability>();

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

        [ContextMenu("Set Extras As Subs")]
        public virtual void SetExtrasAsSubs()
        {
            for (int i = 0; i < ExtraEffects.Count; i++)
            {
                ExtraEffects[i].IsSubEffect = true;
            }
        }

        [ContextMenu("Un-set Extras As Subs")]
        public virtual void UnsetExtrasAsSubs()
        {
            for (int i = 0; i < ExtraEffects.Count; i++)
            {
                ExtraEffects[i].IsSubEffect = false;
            }
        }
    }
}