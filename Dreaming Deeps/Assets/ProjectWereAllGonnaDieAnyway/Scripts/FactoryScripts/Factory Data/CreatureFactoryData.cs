﻿using System.Collections.Generic;
using WereAllGonnaDieAnywayNew.InventorySystem;
using UnityEngine;
using System;

namespace WereAllGonnaDieAnywayNew
{
    [System.Serializable]
    public class CreatureFactoryData
    {
        public int ActorID;
        public float Currency;
        public List<Stat> Statlist;
        public List<ItemFactoryData> ItemsInBag = new List<ItemFactoryData>();

        public List<Info> possibleInfoList = new List<Info>();
       
        public GameObject ActorObject;
        public Vector3 position = Vector3.zero;

        public string Name;
        public int SpriteIndex;

        public  bool isResident = false;

        #region Constructors
        public CreatureFactoryData(GameObject actor)
        {
            Creature target = actor.GetComponent<Creature>();
            Inventory inventory = actor.GetComponent<Inventory>();

            ActorID = target.CreatureID;
            Statlist = target.Stats.StatList;
            ItemsInBag = inventory.ItemsInBag;
            ActorObject = actor;

            SpriteIndex = Int32.Parse(target.transform.GetChild(0).name);

            Name = target.name;

            //add location and ai data later
        }
               
      
        public CreatureFactoryData(int actorID, float currency, List<Stat> statlist, List<ItemFactoryData> itemsInBag, GameObject actor, Vector3 position , int spriteIndex)
        {
            ActorID = actorID;
            Currency = currency;
            Statlist = statlist;
            ItemsInBag = itemsInBag;
            ActorObject = actor;
            this.position = position;
            SpriteIndex = spriteIndex;
        }

        #endregion


        


    }
}
