using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew;
using WereAllGonnaDieAnywayNew.InventorySystem;

public class HarvestPointFactory : IManufacture
{
    GameDatabaseSO data;

    public List<Transform> TargetObjectList = new List<Transform>();

    public HarvestPointFactory(List<Transform> p_TargetObject)
    {
        TargetObjectList = p_TargetObject;
    }

    public void CreateObjects()
    {
        CreateHarvestPoints();
    }
    public void AssignGameDataBase(GameDatabaseSO _data)
    {
        data = _data;
    }

    void CreateHarvestPoints()
    {


       foreach(HarvestPointFactoryDataSO hp in data.HarvestPointDataList)
       {
            DelegateManager.updateCrowdedness += hp.UpdateCrowdedness;
            hp.Crowdedness = 0;

            GameObject go = Object.Instantiate(data.HarvestpointPrefab);
            HarvestPointMonobehaviour hb = go.GetComponent<HarvestPointMonobehaviour>();
            hb.HarvestInventory =  go.GetComponent<Inventory>();
            hb.HarvestPointAbundanceData = hp.AbundanceData;        
            
            foreach(SpawnRateData itm in hp.SpawnRateData.SpawnRateList)
            {
                //Debug.Log(itm.SpawnRate);
                int qty = Random.Range(0, itm.SpawnRate + 1);
                //Debug.Log(itm.ItemName + qty);
                hb.HarvestInventory.AddItemToBag(new ItemFactoryData(itm.ItemName, qty), qty);
                hp.HarvestPointInventory = hb.HarvestInventory;

            }

            hp.HarvestPointInventory.myAbundanceData = hp.AbundanceData;

            DelegateManager.updateHarvestPointItems += hp.UpdateAbundance;

            DelegateManager.updateHarvestPointItems?.Invoke(hp, out bool hasItem);

            //List<ResourceDataSO> resourceList = new List<ResourceDataSO>(hp.AbundanceData.ResourceList);
            //List<ItemFactoryData> itemsInBag = new List<ItemFactoryData>(hb.HarvestInventory.ItemsInBag);

            ////Debug.Log(go.name);

            ////calculateAbundanceHERE
            //for (int i = 0; i < resourceList.Count; i++)
            //{
            //    for (int j = 0; j < itemsInBag.Count; j++)
            //    {
            //        if(itemsInBag[j].ItemName == resourceList[i].Item.ItemName)
            //        {
            //            data.ItemDataMasterList.TryGetValue(resourceList[i].Item.ItemName, out SourceDataItemSO itemSO);
            //            float currentDaysWorth = (float)itemsInBag[j].quantity / (float)itemSO.daysWorthRatio;
            //            //Debug.Log("currentDaysWorth = " + currentDaysWorth + "// quantity: " + itemsInBag[j].quantity + "days worth ratio: " + itemSO.daysWorthRatio);
            //            resourceList[i].Item.quantity = itemsInBag[i].quantity;
            //            resourceList[i].AbundanceValue = currentDaysWorth * 20;
            //        }
            //    }
            //}

            go.transform.parent = GetTargetHarvestLocation(hp.name);
            go.transform.localPosition = Vector2.zero;
       }         


      
    }

    Transform GetTargetHarvestLocation(string name)
    {
        foreach (Transform TargetObject in TargetObjectList)
        {
            if(TargetObject.name == name)
            {
                return TargetObject;
            }
        }

        return TargetObjectList[0];
    }



}
