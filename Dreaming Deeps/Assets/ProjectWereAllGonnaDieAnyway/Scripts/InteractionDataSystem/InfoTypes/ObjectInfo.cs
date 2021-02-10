using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    [CreateAssetMenu(fileName = "New Object Info", menuName = "Info/New Object Info")]
    public class ObjectInfo : Info
    {
        public WorldObject infoWorldObject;

        public ObjectInfoTemplate MyObjectInfoTemplate;

        public List<SerializedDictionaryEntry_StringChain> DefaultStatStringChain = new List<SerializedDictionaryEntry_StringChain>();

        public override string CommunicateRandomInfo()
        {
            DefaultStatStringChain.Clear();

            int randomRelevantStatIndex = Random.Range(0, MyObjectInfoTemplate.RelevantObjectStats.Count);
            Stat targetStat = infoWorldObject.ObjectCreature.Stats.FindStat(MyObjectInfoTemplate.RelevantObjectStats[randomRelevantStatIndex].statType);

            string statInsert = targetStat.statType.ToString();
            string statInfoInsert = HumanizeTypes.Humanize(targetStat.CurrentValue, targetStat.statType, out HumanizeEnum humanizeEnum);

            if(humanizeEnum == HumanizeEnum.NewPhrase)
            {
                DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(statInfoInsert, 2));

                DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(infoWorldObject.ObjectCreature.CharacterName, 0));
                DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(" is ", 1));
            }
            else
            {
                DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(statInsert, 1));
                DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(statInfoInsert, 3));

                DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(infoWorldObject.ObjectCreature.CharacterName + "'s ", 0));
                DefaultStatStringChain.Add(new SerializedDictionaryEntry_StringChain(" is ", 2));
            }

            string finalStatString = Utils.BuildStringChain(DefaultStatStringChain);
            return finalStatString;
        }
    }
}

