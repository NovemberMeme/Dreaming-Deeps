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
        Empty,
        Dead
    }

    [CreateAssetMenu(fileName = "New Character", menuName = "Characters/New Character")]
    public class PartyCharacter : ScriptableObject
    {
        public PartyCharacter CharacterToCopy;

        public Creature MyCreature;

        public float CurrentStamina = 0;
        public float CurrentMana = 0;

        public CombatState MyCombatState = CombatState.Idle;

        public Ability MyAbility;
        public Ability MyAttack;

        public AbilityResponse MyAbilityResponses;

        public virtual void EnableCharacter()
        {
            DelegateController.tick += UpdateCombatValues;
            DelegateController.tick += MyCreature.UpdateStats;
        }

        public virtual void DisableCharacter()
        {
            DelegateController.tick -= UpdateCombatValues;
            DelegateController.tick -= MyCreature.UpdateStats;
        }

        public virtual void JoinBattle()
        {
            CurrentStamina = 0;
            CurrentMana = 0;

            MyCombatState = CombatState.Combat;
        }

        public virtual void UpdateCombatValues(int _tick)
        {
            if (MyCombatState != CombatState.Combat)
                return;

            MyCreature.Stats.FindCurrentStat(STAT_TYPE.AttackSpeed, out float attackSpeed);
            MyCreature.Stats.FindCurrentStat(STAT_TYPE.BaseAttackTime, out float baseAttackTime);
            MyCreature.Stats.FindCurrentStat(STAT_TYPE.ManaRegenPerSecond, out float manaRegenPerSecond);
            MyCreature.Stats.FindCurrentStat(STAT_TYPE.ManaCost, out float manaCost);

            float staminaGained = (1 / Utils.GetTicksPerSecond()) * (attackSpeed / 100);
            CurrentStamina += staminaGained;

            float manaGained = (1 / Utils.GetTicksPerSecond()) * manaRegenPerSecond;
            CurrentMana += manaGained;

            if(CurrentStamina >= baseAttackTime)
            {
                UseAttack();
                CurrentStamina -= baseAttackTime;
            }

            if(CurrentMana >= manaCost)
            {
                UseAbility();
                CurrentMana -= manaCost;
            }
        }

        public virtual void UseAttack()
        {
            PartyCharacter Target = MyAttack.MyTargetType.GetByTargetType(this);

            if (Target == null)
            {
                Debug.Log("No more targets!");
                return;
            }

            AbilityData abilityData = new AbilityData(this, Target, 0, 0, false);
            MyAttack.ApplyEffects(abilityData);
        }

        public virtual void UseAbility()
        {
            PartyCharacter Target = MyAbility.MyTargetType.GetByTargetType(this);

            if (Target == null)
            {
                Debug.Log("No more targets!");
                return;
            }

            AbilityData abilityData = new AbilityData(this, Target, 0, 0, false);
            MyAttack.ApplyEffects(abilityData);
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

        [ContextMenu("Copy Character")]
        public virtual void CopyCharacter()
        {
            MyCreature.Stats = CharacterToCopy.MyCreature.Stats;
        }
    }
}