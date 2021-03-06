﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    public enum STAT_TYPE
    {
        Health,
        Power,
        AttackSpeed,
        _BaseAttackTime,
        _AttackDuration,
        ManaRegenPerSecond,
        _ManaCost,
        StartingMana,
        _AbilityDuration,
        LifeSteal,
        Armor,
        Regen,
        HealAmpli,
        MagicResist,
        SplashDamage,
        MagicDamage,
        ThornsDamage,
        Counter,
        Evasion,
    }
    [System.Serializable]
    public class Stat
    {
        public STAT_TYPE statType;          
      
        private float baseValue = 1 ; 
        public float CurrentValue = 1 ;
        private float maxValue = 100f ;
        //[HideInInspector]
        public List<EntityStatModifier> Modifiers = new List<EntityStatModifier>();


        public Stat(STAT_TYPE statType, float baseValue)
        {
          
            this.statType = statType;
            this.CurrentValue = baseValue;
            this.BaseValue = baseValue;
            Modifiers.Clear();
        }

    
        public float BaseValue { get => baseValue; set => baseValue = value; }
        public float MaxValue { get => maxValue; set => maxValue = value; }

        public void ApplyModifiers()
        {
            foreach(EntityStatModifier mod in Modifiers)
            {
                mod.Apply(this);
            }

        }
        public void RemoveModifier(EntityStatModifier mod)
        {
            Modifiers.Remove(mod);
            ApplyModifiers();
        }

        public void Reset()
        {
            CurrentValue = baseValue;
        }

       
    }
}
