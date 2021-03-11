using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Ability Effect", menuName = "Abilities/New Ability Effect")]
    public class Ability : ScriptableObject
    {
        public TargetType MyTargetType;

        public float WeaponBaseAttackTime;
        public float ManaCost;
        public float BonusStartingMana;

        public List<AbilityTag> MyTags = new List<AbilityTag>();
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

        public virtual void EquipAbility(PartyCharacter _user)
        {
            _user.MyCreature.Stats.ModifyCurrentStat(WereAllGonnaDieAnywayNew.STAT_TYPE.StartingMana, BonusStartingMana);
            _user.MyCreature.Stats.SetCurrentStat(WereAllGonnaDieAnywayNew.STAT_TYPE._BaseAttackTime, WeaponBaseAttackTime);
        }

        public virtual void UnEquipAbility(PartyCharacter _user)
        {

        }
    }
}