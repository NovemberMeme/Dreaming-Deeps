using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Tile Ability", menuName = "Abilities/New Tile Ability")]
    public class TileAbility : ScriptableObject
    {
        public EnergyData TileAbilityCost;

        public List<AbilityTag> MyTags = new List<AbilityTag>();
        public List<TileAbility> ExtraEffects = new List<TileAbility>();

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