using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew.InventorySystem;


/// <summary>
/// TODO:
/// - Finder methods
/// </summary>
namespace WereAllGonnaDieAnywayNew
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Game Data Base SO", menuName = "Factory/New Game Data Base SO")]
    public class GameDatabaseSO : ScriptableObject
    {
        /// <summary>
        /// This Data in this class will be converted into a json file for persistence
        /// </summary>

        
        [Header("Item List")]
        public List<SourceDataItemSO> ItemTemplates = new List<SourceDataItemSO>();
        [HideInInspector]
        public static List<string> ItemNameMasterList = new List<string>();
        public Dictionary<string, SourceDataItemSO> ItemDataMasterList = new Dictionary<string, SourceDataItemSO>();

        [Header("# CREATURE SETUP #")]

        [Header("Creature Prefabs")]
        public GameObject CitizenBasePrefab;
        public GameObject ResidentBasePrefab;

        [SerializeField]
        public GlobalInfoProvider GlobalInfoProvider;

        [Header("Visual prefabs")]
        public List<GameObject> CreatureSpritePrefabList = new List<GameObject>();

        [Header("Creature Base Setup")]
        public List<STAT_TYPE> CreatureDefaultStatList = new List<STAT_TYPE>();

        [Header("Potential Names")]
        public List<string> CreatureNamesList = new List<string>();

        [Header("List of all Creatures that exists in the world inclusive of residents")]
        public List<CreatureFactoryData> CitizensMasterList = new List<CreatureFactoryData>();

        [Header("List of all HarvestPoint")]
        public List<HarvestPointFactoryDataSO> HarvestPointDataList = new List<HarvestPointFactoryDataSO>();
        public GameObject HarvestpointPrefab;
       
      
        private void OnEnable()
        {
            Initialize();
        }
        public void Initialize()
        {

            ItemNameMasterList.Clear(); //double check names or just clear before transfering json data
            ItemDataMasterList.Clear();
            foreach (SourceDataItemSO template in ItemTemplates)
            {

                if (!ItemDataMasterList.ContainsKey(template.name) && !ItemNameMasterList.Contains(template.name))
                {
                    ItemNameMasterList.Add(template.name);
                    ItemDataMasterList.Add(template.name, template);
                }
                else
                {
                    ItemTemplates.Remove(template);
                    continue;
                }

                // CreateDataListEntryItem(template, template.type);
            }


        }


        public bool FindCitizenDataFromList(int creatureID, out CreatureFactoryData actor )       
        {
            
            foreach (CreatureFactoryData ad in CitizensMasterList)
            {
                if (ad.ActorID == creatureID)
                {
                    actor = ad;
                    return true;
                }
            }

            Debug.LogWarning("ID Does not Exist");
            actor = null;
            return false;

        }

        //string array that returns the list of item names used in dropdown
        public static string[] GET_ITEM_LIST()
        {
            return ItemNameMasterList.ToArray();
        }

       


    }
}
