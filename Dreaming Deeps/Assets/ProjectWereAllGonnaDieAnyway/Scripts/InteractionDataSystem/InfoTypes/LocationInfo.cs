using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew.InventorySystem;

namespace WereAllGonnaDieAnywayNew
{
    [CreateAssetMenu(fileName = "New Location Info", menuName = "Info/New Location Info")]
    public class LocationInfo : Info
    {
        public HarvestPointFactoryDataSO MyLocation;

        public List<SerializedDictionaryEntry_StringChain> DefaultCrowdednessStringChain = new List<SerializedDictionaryEntry_StringChain>();
        public List<SerializedDictionaryEntry_StringChain> DefaultAbundanceListStringChain = new List<SerializedDictionaryEntry_StringChain>();

        public override string CommunicateRandomInfo()
        {
            DefaultAbundanceListStringChain.Clear();
            DefaultCrowdednessStringChain.Clear();

            int randomLocationInfoType = UnityEngine.Random.Range(0, 2);

            string locationInsert = MyLocation.LocationName;

            switch (randomLocationInfoType)
            {
                case 0:
                    string crowdednessInsert = HumanizeTypes.HumanizeLowHigh(MyLocation.Crowdedness);

                    DefaultCrowdednessStringChain.Add(new SerializedDictionaryEntry_StringChain("Crowdedness", 1));
                    DefaultCrowdednessStringChain.Add(new SerializedDictionaryEntry_StringChain(crowdednessInsert, 3));

                    DefaultCrowdednessStringChain.Add(new SerializedDictionaryEntry_StringChain(locationInsert + "'s ", 0));
                    DefaultCrowdednessStringChain.Add(new SerializedDictionaryEntry_StringChain(" is ", 2));

                    string finalCrowdednessString = Utils.BuildStringChain(DefaultCrowdednessStringChain);
                    return finalCrowdednessString;
                case 1:
                    int randomRelevantResourceIndex = Random.Range(0, MyLocation.AbundanceData.ResourceList.Count);
                    ResourceDataSO targetResourceAbundance = MyLocation.AbundanceData.ResourceList[randomRelevantResourceIndex];

                    string resourceInsert = targetResourceAbundance.Item.ItemName;
                    string resourceInfoInsert = HumanizeTypes.HumanizeLowHigh(targetResourceAbundance.AbundanceValue);

                    DefaultAbundanceListStringChain.Add(new SerializedDictionaryEntry_StringChain(resourceInsert + " stock", 1));
                    DefaultAbundanceListStringChain.Add(new SerializedDictionaryEntry_StringChain(resourceInfoInsert, 3));

                    DefaultAbundanceListStringChain.Add(new SerializedDictionaryEntry_StringChain(locationInsert + "'s ", 0));
                    DefaultAbundanceListStringChain.Add(new SerializedDictionaryEntry_StringChain(" is ", 2));

                    string finalAbundanceListString = Utils.BuildStringChain(DefaultAbundanceListStringChain);
                    return finalAbundanceListString;
            }

            return "I don't know...";
        }
    }
}
