using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace WereAllGonnaDieAnywayNew
{
    public enum CREATURE_TYPE
    {
        PLAYER,
        NPC,
        OBJECT,
        NONE
    }
  
    //everything that can get affected by items is an actor 
    [System.Serializable]
    public  class Creature
    {
        public string CharacterName;
        [HideInInspector]
        public int CreatureID;
        [HideInInspector]
        public CREATURE_TYPE Type = CREATURE_TYPE.NONE;

        public EntityStatHandler Stats = new EntityStatHandler();

        // Make this listen to Tick instead so it's not a monobehavior

        public virtual void UpdateStats(int _tick)
        {
            for (int i = 0; i < Stats.StatList.Count; i++)
            {
                Stats.StatList[i].ApplyModifiers();
            }
        }
    }

}
