using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew.InventorySystem;
using WereAllGonnaDieAnywayNew;

  [System.Serializable]
    [CreateAssetMenu(fileName = "Household Database", menuName = "Factory/New Household Database SO")]
    public class HouseHoldDataBaseSO : ScriptableObject
    {
        public GameDatabaseSO gamedata;

        [Space]
        [SerializeField]
        List<ItemFactoryData> SharedResourceList = new List<ItemFactoryData>();

      
        static Stat[] StatList =
        {
            new Stat(STAT_TYPE.cleanliness,50),
            new Stat(STAT_TYPE.happiness,50),
            new Stat(STAT_TYPE.health,50),
            new Stat(STAT_TYPE.infection_level,0)

        };
        [SerializeField]
        public EntityStatHandler HouseHoldStats = new EntityStatHandler(StatList);        
       
       [HideInInspector]
        public List<int> ResidentList = new List<int>();      
       
        public List<Residentdata> ResidentDataList = new List<Residentdata>();
        
        [HideInInspector]
        public Creature SelectedActor;
        [HideInInspector]
        public List<Creature> RuntimeResidentList = new List<Creature>();


        #region Public Methods

        // by default task set to none

       

        public void AssignTasks(HOUSEHOLD_TASKS task)
        {
            switch (task)
            {
                case HOUSEHOLD_TASKS.COOKING:

                    findResidentData(SelectedActor.CreatureID).AssignedTask = HOUSEHOLD_TASKS.COOKING;
                    SelectedActor = null;
                    break;

                case HOUSEHOLD_TASKS.CLEANING:

                    findResidentData(SelectedActor.CreatureID).AssignedTask = HOUSEHOLD_TASKS.CLEANING;
                    SelectedActor = null;
                    break;

                case HOUSEHOLD_TASKS.MASCOT:

                    findResidentData(SelectedActor.CreatureID).AssignedTask = HOUSEHOLD_TASKS.MASCOT;
                    SelectedActor = null;
                    break;

                case HOUSEHOLD_TASKS.NONE:
                    findResidentData(SelectedActor.CreatureID).AssignedTask = HOUSEHOLD_TASKS.NONE;
                    SelectedActor = null;
                    break;

            }
        }




        /// <summary>
        /// Add a resident on runtime 
        /// </summary>
        /// <param name="resident"></param>
        public void AddRuntimeResident(Creature resident)
        {
            RuntimeResidentList.Add(resident);
        }

        public void ClearRuntimeResidentList()
        {
            RuntimeResidentList.Clear();
        }

        public Creature FindRuntimeResident(int id)
        {
            for (int i = 0; i < RuntimeResidentList.Count; i++)
            {
                if (RuntimeResidentList[i].GetComponent<Creature>().CreatureID == id)
                {
                    return RuntimeResidentList[i].GetComponent<Creature>();
                }
            }

            Debug.Log("Resident Not Found");
            return null;
        }

        //
        public void SetSelectedActor(Creature caller)
        {
            SelectedActor = caller;
        }


        Residentdata findResidentData(int residentID)
        {
            for (int i = 0; i < ResidentDataList.Count; i++)
            {
                if (ResidentDataList[i].CreatureData.ActorID == residentID)
                {
                    return ResidentDataList[i];
                }
            }

            return null;
        }


        //HouseMate Management

        public void AddNewResident(CreatureFactoryData actor)
        {
            if (findIndexOfResident(actor.ActorID) > 0)
            {
                Debug.Log("Resident Already Exists");
                return;
            }

            ResidentList.Add(actor.ActorID);
        }

        public void AddNewResident(int id)
        {
            if (findIndexOfResident(id) > 0)
            {
                Debug.Log("Resident Already Exists");
                return;
            }

            if (gamedata.FindCitizenDataFromList(id, out CreatureFactoryData newResident))
            {
                ResidentDataList.Add(new Residentdata(newResident));
                ResidentList.Add(id);
            }
        }

        public void AddNewResident()
        {
            int id = Random.Range(0, gamedata.CitizensMasterList.Count);
            while (findIndexOfResident(id) > -1)
            {
                if (ResidentList.Count >= gamedata.CitizensMasterList.Count)
                {
                    Debug.Log("nothing else to add");
                    return;
                }

                id = Random.Range(0, gamedata.CitizensMasterList.Count);
                // Debug.Log("Resident Already Exists");

            }

            if (gamedata.FindCitizenDataFromList(id, out CreatureFactoryData newResident))
            {
                Residentdata newGuy = new Residentdata(newResident);

                var taskListArray = System.Enum.GetValues(typeof(HOUSEHOLD_TASKS));
                int i = Random.Range(0, taskListArray.Length);
                newGuy.WantToDoTask = (HOUSEHOLD_TASKS)i;

                ResidentDataList.Add(newGuy);
                newResident.isResident = true;
                ResidentDataList[ResidentDataList.Count - 1].CreatureData.ActorObject = gamedata.ResidentBasePrefab;
                //Debug.Log;
                ResidentList.Add(id);
            }
        }
        public void KickResident(CreatureFactoryData actor)
        {
            int index = findIndexOfResident(actor.ActorID);
            ResidentList.RemoveAt(index);
        }




        // Resource Management

        public void AddItem(Inventory source, int qty, string ItemName)
        {
            source.TransferItemData(ItemName, qty, out ItemFactoryData targetItem);
            addItemToStock(targetItem);
        }

        public void ShareItem(Inventory targetInventory, int qty, string ItemName)
        {
            // transfer item from common to target
        }


        #endregion

        #region Custom Callbacks

        void addItemToStock(ItemFactoryData itemtoAdd)
        {
            if (SharedResourceList.Count < 0) return;

            foreach (ItemFactoryData item in SharedResourceList)
            {
                if (itemtoAdd.ItemName == item.ItemName)
                {
                    item.quantity += itemtoAdd.quantity;
                    return;
                }
            }

            SharedResourceList.Add(itemtoAdd);
            return;
        }


        void removeItemFromStock(string itemName, int qty)
        {
            int index = findIndexOfItem(itemName);
            if (index < 0 || SharedResourceList[index].quantity < qty) return;

            SharedResourceList[index].quantity -= qty;

            if (SharedResourceList[index].quantity <= 0)
            {
                SharedResourceList.RemoveAt(index);
            }


        }

        void deleteItemFromStock(string itemName)
        {
            int index = findIndexOfItem(itemName);
            if (index < 0) return;
            SharedResourceList.RemoveAt(index);

        }

        //returns -1 if item or resident isn't found
        int findIndexOfItem(string itemName)
        {
            if (SharedResourceList.Count < 0) return -1;

            for (int i = 0; i < SharedResourceList.Count; i++)
            {
                if (itemName == SharedResourceList[i].ItemName)
                {
                    return i;
                }
            }

            return -1;
        }

        int findIndexOfResident(int npcId)
        {
            //  if (ResidentList.Count < 0) return -1;

            for (int i = 0; i < ResidentList.Count; i++)
            {
                if (npcId == ResidentList[i])
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion



    }


