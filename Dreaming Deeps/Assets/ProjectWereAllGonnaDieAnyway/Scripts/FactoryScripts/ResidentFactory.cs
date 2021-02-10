using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WereAllGonnaDieAnywayNew.InventorySystem;

namespace WereAllGonnaDieAnywayNew
{
    public class ResidentFactory:IManufacture
    {
        GameDatabaseSO data;
        HouseHoldDataBaseSO houseHoldData;

        #region initializations

        public ResidentFactory()
        { }
        public ResidentFactory(GameDatabaseSO data, HouseHoldDataBaseSO houseHoldData)
        {
            this.data = data;
            this.houseHoldData = houseHoldData;
        }

        public void AssignGameDatabase(GameDatabaseSO data)
        {
            this.data = data;         
        }
        public void AssignHouseHoldData(HouseHoldDataBaseSO houseHoldData)
        {           
            this.houseHoldData = houseHoldData;
        }

        #endregion

        #region Monobehaviour Callbacks


        public void CreateObjects()
        {
            LoadResidents();
        }       

        private void LoadResidents()
        {
            houseHoldData.ClearRuntimeResidentList();
            //foreach (Residentdata resident in houseHoldData.ResidentDataList)
            //{
            //    SpawnResident(resident.CreatureData.ActorID, out Creature thisActor);               
            //    houseHoldData.SetSelectedActor(thisActor);
            //    houseHoldData.AssignTasks(resident.AssignedTask);
            //}

            for (int i = 0; i < houseHoldData.ResidentList.Count; i++)
            {
                SpawnResident(houseHoldData.ResidentList[i], out Creature thisActor);
                houseHoldData.SetSelectedActor(thisActor);
                houseHoldData.AssignTasks(houseHoldData.ResidentDataList[i].AssignedTask);
            }
        }

        #endregion

        public void SpawnResident(int id, out Creature actor)
        {
           
            if (!ReadActorData(id, out CreatureFactoryData tmp))
            {
                Debug.LogWarning("Actor data does not exists");
                actor = null;
                return;
            }
            else
            {
                
                GameObject go = Object.Instantiate(houseHoldData.gamedata.ResidentBasePrefab);
                actor = go.GetComponent<Creature>();
                actor.Stats.StatList = tmp.Statlist;
                actor.CreatureID = tmp.ActorID;

                GameObject visual = Object.Instantiate(data.CreatureSpritePrefabList[tmp.SpriteIndex]);
                visual.transform.SetParent(go.transform);
                visual.transform.localPosition = Vector3.zero;
                visual.name = tmp.SpriteIndex.ToString();

                go.name = tmp.Name;
                go.transform.position = tmp.position;


                houseHoldData.AddRuntimeResident(actor);
                

            }

        }


        public bool ReadActorData(int id, out CreatureFactoryData actor)
        {
            foreach (CreatureFactoryData ad in houseHoldData.gamedata.CitizensMasterList)
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



        public void addNewResident()
        {          
            houseHoldData.AddNewResident();
        }

    
        public void clearResidents()
        {
            houseHoldData.ResidentList.Clear();
            houseHoldData.ResidentDataList.Clear();

        }
              
    }

    public enum HOUSEHOLD_TASKS
    {
        COOKING,
        CLEANING,
        MASCOT,
        NONE
    }

}



