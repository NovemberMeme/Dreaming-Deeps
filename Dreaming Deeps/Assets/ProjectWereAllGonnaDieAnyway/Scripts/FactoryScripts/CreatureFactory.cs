using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew.InventorySystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace WereAllGonnaDieAnywayNew
{
    /// <summary>
    /// SPAWNS ACTORS!!!!
    /// </summary>
    public class CreatureFactory : IManufacture
    {
        GameDatabaseSO data;
        //update and export json here? 
        // i dunno 
        // nothing makes sense XD

  
        public void CreateRandomCitizen()
        {
            #if UNITY_EDITOR

            EditorUtility.SetDirty(data);

            #endif

            int randomItemAmount = Random.Range(0,11);
            float randomBaseStatValue = Random.Range(0f,10f);
            int randomSprite = Random.Range(0, data.CreatureSpritePrefabList.Count);
            int randomName = Random.Range(0, data.CreatureNamesList.Count);
            CreatureFactoryData citizen = new CreatureFactoryData(data.CitizensMasterList.Count,
                100f,
                new List<Stat>(),
                new List<ItemFactoryData>(),
                data.CitizenBasePrefab, // change to whichever base depending on usage
                Vector3.zero,
                randomSprite
                );
            citizen.Name = data.CreatureNamesList[randomName] + " " + data.CreatureNamesList[Random.Range(0, data.CreatureNamesList.Count)];


            createRandomItems(citizen, randomItemAmount);

            createRandomValueStats(citizen,5, randomBaseStatValue);
            data.CitizensMasterList.Add(citizen);
        }

        #region Constructors

        public CreatureFactory()
        {         
        }

        public CreatureFactory(GameDatabaseSO gameDataBase)
        {
            data = gameDataBase;
        }

        #endregion
        #region Setup

        public void CreateObjects()
        {
            SpawnAllCitizens();
        }
        public void AssignGameDataBase(GameDatabaseSO _data)
        {
            data = _data;
        }
        void createRandomItems(CreatureFactoryData creature, int maxAmount)
        {
            float amt = 0;
            foreach (SourceDataItemSO item in data.ItemTemplates)
            {
                amt = Random.Range(0, maxAmount);
                creature.ItemsInBag.Add(new ItemFactoryData(item.name, (int)amt));
            }
        }
        void createRandomValueStats(CreatureFactoryData creature, float min,float baseValue)
        {
            float val;
            foreach(STAT_TYPE stat in data.CreatureDefaultStatList)
            {               
                Stat statToAdd = new Stat(stat, baseValue);
                val = Random.Range(min, statToAdd.MaxValue);
                statToAdd.CurrentValue = (val);
                creature.Statlist.Add(statToAdd);
                
            }

        }
        #endregion

        void SpawnAllCitizens()
        {
            data.GlobalInfoProvider.currentCitizens.Clear();

            for (int i = 0; i < data.CitizensMasterList.Count; i++)
            {
                if (!data.CitizensMasterList[i].isResident)
                {
                    SpawnCitizen(i);
                }
              
            }
        }

        public void SpawnCitizen(int id)
        {
            if (!ReadActorData(id, out CreatureFactoryData tmp))
            {
                Debug.LogWarning("Actor data does not exists");
                return;
            }else
            {

                GameObject go = Object.Instantiate(tmp.ActorObject);

                GameObject visual = Object.Instantiate(data.CreatureSpritePrefabList[tmp.SpriteIndex]);

                Creature actor = go.GetComponent<Creature>();
                actor.Type = CREATURE_TYPE.NPC;
                actor.Stats = new EntityStatHandler(tmp.Statlist);
                go.GetComponent<WereAllGonnaDieAnywayNew.InventorySystem.Inventory>().ItemsInBag = tmp.ItemsInBag;    
               
                actor.CreatureID = tmp.ActorID;
                go.name = tmp.Name;

                visual.transform.SetParent(go.transform);
                visual.transform.localPosition = Vector3.zero;
                visual.name = tmp.SpriteIndex.ToString();
                go.transform.position = tmp.position;

                data.GlobalInfoProvider.currentCitizens.Add(go.GetComponent<Communicator>());
            }         
            
        }

        #region Move this to the class that handles data
        //public void CreatActorData(GameObject actor)
        //{
        //    if(!ReadActorData(actor.GetComponent<Creature>().CreatureID, out CreatureFactoryData tmp))
        //    {
        //        data.CitizensMasterList.Add( new CreatureFactoryData(actor, data.CitizensMasterList.Count) );
        //    }
              
        //}

        public bool ReadActorData(int id, out CreatureFactoryData actor)
        {
            foreach(CreatureFactoryData ad in data.CitizensMasterList)
            {
                if (ad.ActorID == id)
                {
                    actor = ad;
                    return true;
                }
            }
            Debug.LogWarning("ID Does not Exist");
            actor = null;
            return false;
        }


        /// <summary>
        /// UpdatesCreture data in CitizenMasterList
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        //add an observer to call this or delegate
        public bool UpdateCreatureData(GameObject actor)
        {
            if (!ReadActorData(actor.GetComponent<Creature>().CreatureID, out CreatureFactoryData tmp))
            {
                Debug.LogWarning("Creature data does not exists");
                return false;
            }else
            {
                Creature target = actor.GetComponent<Creature>();
                WereAllGonnaDieAnywayNew.InventorySystem.Inventory inventory = actor.GetComponent<WereAllGonnaDieAnywayNew.InventorySystem.Inventory>();
                tmp.ItemsInBag = inventory.ItemsInBag;
                tmp.Statlist = target.Stats.StatList;
                tmp.position = actor.transform.position;
                //update location data;

                return true;
            }

        }

        /// <summary>
        /// Updates Creature Position in CitizenMasterList
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
       
        public bool UpdateCreaturePosition(GameObject actor)
        {
            if (!ReadActorData(actor.GetComponent<Creature>().CreatureID, out CreatureFactoryData tmp))
            {
                Debug.LogWarning("Actor data does not exists");
                return false;
            }else
            {                              
                tmp.position = actor.transform.position;
                //update location data;
                return true;
            }


        }
        #endregion


    }
}