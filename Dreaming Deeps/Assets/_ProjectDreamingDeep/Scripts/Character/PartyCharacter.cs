using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew;

namespace DreamingDeep
{
    public enum CombatState
    {
        Idle,
        Combat,
        Dead
    }

    public class PartyCharacter : MonoBehaviour
    {
        public Creature MyCreature;

        public float CurrentStamina = 0;
        public float CurrentMana = 0;

        public CombatState MyCombatState = CombatState.Idle;

        public AbilityResponse MyAbilityResponses;

        protected virtual void Update()
        {

        }

        public virtual void JoinBattle()
        {
            CurrentStamina = 0;
            CurrentMana = 0;

            MyCombatState = CombatState.Combat;
        }

        public virtual void UpdateCombatValues()
        {
            MyCreature.Stats.FindCurrentStat(STAT_TYPE.AttackSpeed, out float attackSpeed);
            MyCreature.Stats.FindCurrentStat(STAT_TYPE.ManaRegen, out float manaRegen);
        }

        public virtual void GetTargetedByAbilityResponse(AbilityData _abilityData)
        {
            MyAbilityResponses.RespondToAbility(_abilityData);
        }

        public virtual void GetDamaged(AbilityData _abilityData)
        {
            MyCreature.Stats.FindCurrentStat(STAT_TYPE.Armor, out float armor);

            float damageTaken = _abilityData.PhysicalDamageAmount - armor;

            MyCreature.Stats.ModifyCurrentStat(STAT_TYPE.Health, damageTaken);
            MyCreature.Stats.ModifyCurrentStat(STAT_TYPE.Health, _abilityData.MagicalDamageAmount);
        }
    }
}