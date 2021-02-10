using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew.InventorySystem;

namespace WereAllGonnaDieAnywayNew
{
    [System.Serializable]
    public class SerializedDictionaryEntry_Stat
    {
        public Stat ActorStat;
        public float Weight;

        public SerializedDictionaryEntry_Stat(Stat targetStat, float startingValue)
        {
            ActorStat = targetStat;
            Weight = startingValue;
        }
    }

    [System.Serializable]
    public class SerializedDictionaryEntry_StringChain
    {
        public string StringText;
        public int Index;

        public SerializedDictionaryEntry_StringChain(string stringText, int startingValue)
        {
            StringText = stringText;
            Index = startingValue;
        }
    }

    [System.Serializable]
    public class SerializedDictionaryEntry_ItemType
    {
        public ItemFactoryData ItemType;
        public float Value;

        public SerializedDictionaryEntry_ItemType(ItemFactoryData targetTemplate, float startingQuantity)
        {
            ItemType = targetTemplate;
            Value = startingQuantity;
        }
    }
}