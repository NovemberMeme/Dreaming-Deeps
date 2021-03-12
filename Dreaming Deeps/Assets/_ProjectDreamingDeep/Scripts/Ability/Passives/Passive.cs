using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Passive", menuName = "Abilities/New Passive")]
    public class Passive : ScriptableObject
    {
        public List<PartyCharacter> AffectedTargets = new List<PartyCharacter>();

        public List<Passive> ExtraEffects = new List<Passive>();

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

        public virtual void EquipAbility(PartyCharacter _user)
        {

        }

        public virtual void UnEquipAbility(PartyCharacter _user)
        {

        }
    }
}