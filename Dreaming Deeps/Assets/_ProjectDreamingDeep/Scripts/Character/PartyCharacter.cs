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
        ChargingAttack,
        Attacking,
        ChargingAbility,
        Casting,
        Dead,
    }

    [CreateAssetMenu(fileName = "New Character", menuName = "Characters/New Character")]
    public class PartyCharacter : ScriptableObject
    {
        public DataBaseSO DB;

        public PartyCharacter CharacterToCopy;

        public Creature MyCreature;

        public float CurrentStamina = 0;
        public float CurrentMana = 0;

        public List<CombatState> MyCombatStates = new List<CombatState>();

        public Ability MyAttack;
        public Ability FrontlinerAbility;
        public Ability BacklinerAbility;

        public AbilityResponse MyAbilityResponses;

        public virtual void EnableCharacter()
        {
            DelegateController.tick += UpdateCombatValues;
            DelegateController.tick += MyCreature.UpdateStats;
            MyCombatStates.Add(CombatState.Idle);
        }

        public virtual void DisableCharacter()
        {
            DelegateController.tick -= UpdateCombatValues;
            DelegateController.tick -= MyCreature.UpdateStats;
            MyCombatStates.Clear();
        }

        public virtual void JoinBattle()
        {
            CurrentStamina = 0;

            MyCreature.Stats.FindCurrentStat(STAT_TYPE.StartingMana, out float startingMana);
            CurrentMana = startingMana;

            MyCombatStates.Remove(CombatState.Idle);
            MyCombatStates.Add(CombatState.Combat);
        }

        public virtual void UpdateCombatValues(int _tick)
        {
            if (!MyCombatStates.Contains(CombatState.Combat))
                return;

            MyCreature.Stats.FindCurrentStat(STAT_TYPE.AttackSpeed, out float attackSpeed);
            MyCreature.Stats.FindCurrentStat(STAT_TYPE._BaseAttackTime, out float baseAttackTime);
            MyCreature.Stats.FindCurrentStat(STAT_TYPE._AttackDuration, out float attackDuration);
            MyCreature.Stats.FindCurrentStat(STAT_TYPE.ManaRegenPerSecond, out float manaRegenPerSecond);
            MyCreature.Stats.FindCurrentStat(STAT_TYPE.StartingMana, out float startingMana);
            MyCreature.Stats.FindCurrentStat(STAT_TYPE._ManaCost, out float manaCost);
            MyCreature.Stats.FindCurrentStat(STAT_TYPE._AbilityDuration, out float abilityDuration);

            if (MyCombatStates.Contains(CombatState.ChargingAttack))
            {
                float staminaGained = (1 / Utils.GetTicksPerSecond()) * (attackSpeed / 100);
                CurrentStamina += staminaGained;
            }

            if (MyCombatStates.Contains(CombatState.Attacking))
            {
                float attackDurationInTicks = attackDuration * Utils.GetTicksPerSecond();
                float staminaLost = baseAttackTime / attackDurationInTicks;
                CurrentStamina -= staminaLost;
            }

            if (MyCombatStates.Contains(CombatState.ChargingAbility))
            {
                float manaGained = (1 / Utils.GetTicksPerSecond()) * manaRegenPerSecond;
                CurrentMana += manaGained;
            }

            if (MyCombatStates.Contains(CombatState.Casting))
            {
                float abilityDurationInTicks = abilityDuration * Utils.GetTicksPerSecond();
                float manaLost = manaCost / abilityDurationInTicks;
                CurrentMana -= manaLost;
            }

            if(CurrentStamina >= baseAttackTime)
            {
                CurrentStamina = baseAttackTime;
                float secondsPerAttack = baseAttackTime / (attackSpeed / 100);
                float calculatedAttackDuration = Mathf.Clamp(secondsPerAttack * DB.BaseAttackDuration, 0, DB.MaxAttackDuration);
                MyCreature.Stats.SetCurrentStat(STAT_TYPE._AttackDuration, calculatedAttackDuration);

                MyCombatStates.Remove(CombatState.ChargingAttack);
                MyCombatStates.Add(CombatState.Attacking);

                UseAttack();
            }

            if(CurrentStamina < 0)
            {
                CurrentStamina = 0;
                MyCombatStates.Remove(CombatState.Attacking);
                MyCombatStates.Add(CombatState.ChargingAttack);
            }

            if(CurrentMana >= manaCost)
            {
                CurrentMana = manaCost;
                float secondsPerAbility = manaCost / manaRegenPerSecond;
                float calculatedAbilityDuration = Mathf.Clamp(secondsPerAbility * DB.BaseAbilityDuration, 0, DB.MaxAbilityDuration);
                MyCreature.Stats.SetCurrentStat(STAT_TYPE._AbilityDuration, calculatedAbilityDuration);

                MyCombatStates.Remove(CombatState.ChargingAbility);
                MyCombatStates.Add(CombatState.Casting);

                UseAbility();
            }

            if(CurrentMana < 0)
            {
                CurrentMana = 0;
                MyCombatStates.Remove(CombatState.Casting);
                MyCombatStates.Add(CombatState.ChargingAbility);
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

            AbilityData abilityData = new AbilityData(this, Target, 0, 0, MyAttack.MyTags);
            MyAttack.ApplyEffects(abilityData);
        }

        public virtual void UseAbility()
        {
            PartyCharacter Target = FrontlinerAbility.MyTargetType.GetByTargetType(this);

            if (Target == null)
            {
                Debug.Log("No more targets!");
                return;
            }

            AbilityData abilityData = new AbilityData(this, Target, 0, 0, FrontlinerAbility.MyTags);
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

        [ContextMenu("Copy Character Stats")]
        public virtual void CopyCharacterStats()
        {
            MyCreature.Stats = CharacterToCopy.MyCreature.Stats;
        }
    }
}