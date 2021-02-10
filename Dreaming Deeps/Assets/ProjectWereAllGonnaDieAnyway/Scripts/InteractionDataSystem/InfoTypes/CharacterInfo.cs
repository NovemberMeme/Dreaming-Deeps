using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew.InventorySystem;

namespace WereAllGonnaDieAnywayNew
{
    [CreateAssetMenu(fileName = "New Character Info", menuName = "Info/New Character Info")]
    public class CharacterInfo : Info
    {
        //public AI_Creature InfoAI;

        public CharacterInfoTemplate MyCharInfoTemplate;

        public List<ItemFactoryData> RelevantResources = new List<ItemFactoryData>();

        public List<SerializedDictionaryEntry_StringChain> DefaultStatStringChain = new List<SerializedDictionaryEntry_StringChain>();
        public List<SerializedDictionaryEntry_StringChain> DefaultResourceStringChain = new List<SerializedDictionaryEntry_StringChain>();

        private void UpdateRelevantResources()
        {
            RelevantResources.Clear();

            //for (int i = 0; i < InfoAI.citizenInventory.ItemsInBag.Count; i++)
            //{
            //    RelevantResources.Add(InfoAI.citizenInventory.ItemsInBag[i]);
            //}
        }

        public override string CommunicateRandomInfo()
        {
            //UpdateRelevantResources();
            //DefaultStatStringChain.Clear();
            //DefaultResourceStringChain.Clear();

            //int randomCharInfoType = UnityEngine.Random.Range(0, 2);

            //switch (randomCharInfoType)
            //{
            //    case 0:
            //        int randomRelevantStatIndex = Random.Range(0, MyCharInfoTemplate.RelevantActorStats.Count);
            //        Stat targetStat = InfoAI.CitizenCreature.Stats.FindStat(MyCharInfoTemplate.RelevantActorStats[randomRelevantStatIndex].statType);

            //        string statInsert = targetStat.statType.ToString();
            //        string statInfoInsert = HumanizeTypes.Humanize(targetStat.CurrentValue, targetStat.statType, out HumanizeEnum humanizeEnum);

            //        if(humanizeEnum == HumanizeEnum.NewPhrase)
            //        {
            //            DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(statInfoInsert, 2));

            //            //DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(InfoAI.CitizenCreature.CharacterName, 0));
            //            DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(" is ", 1));
            //        }
            //        else
            //        {
            //            DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(statInsert, 1));
            //            DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(statInfoInsert, 3));

            //            //DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(InfoAI.CitizenCreature.CharacterName + "'s ", 0));
            //            DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(" is ", 2));
            //        }

            //        string finalStatString = Utils.BuildStringChain(DefaultStatStringChain);
            //        return finalStatString;
            //    case 1:
            //        int randomRelevantResourceIndex = Random.Range(0, RelevantResources.Count);
            //        ItemFactoryData targetResource = RelevantResources[randomRelevantResourceIndex];

            //        string resourceInsert = targetResource.ItemName;
            //        string resourceInfoInsert = HumanizeTypes.HumanizeLowHigh(targetResource.quantity);

            //        DefaultResourceStringChain.Add(new SerializedDictionaryEntry_StringChain(resourceInsert + " stock", 1));
            //        DefaultResourceStringChain.Add(new SerializedDictionaryEntry_StringChain(resourceInfoInsert, 3));

            //        //DefaultResourceStringChain.Add(new SerializedDictionaryEntry_StringChain(InfoAI.CitizenCreature.CharacterName + "'s ", 0));
            //        DefaultResourceStringChain.Add(new SerializedDictionaryEntry_StringChain(" is ", 2));

            //        string finalResourceString = Utils.BuildStringChain(DefaultResourceStringChain);
            //        return finalResourceString;
            //}

            return "I don't know...";
        }

        private void HumanizeStatString(float statValue)
        {

        }
    }
}