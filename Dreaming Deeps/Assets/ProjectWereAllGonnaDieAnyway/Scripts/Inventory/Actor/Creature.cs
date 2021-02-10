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
    public  class Creature : MonoBehaviour
    {
        public string CharacterName;
        [HideInInspector]
        public int CreatureID;
        [HideInInspector]
        public CREATURE_TYPE Type = CREATURE_TYPE.NONE;

        public EntityStatHandler Stats = new EntityStatHandler();

        private void Update()
        {
            for (int i = 0; i < Stats.StatList.Count; i++)
            {
                Stats.StatList[i].ApplyModifiers();
            }
        }
    }

}
