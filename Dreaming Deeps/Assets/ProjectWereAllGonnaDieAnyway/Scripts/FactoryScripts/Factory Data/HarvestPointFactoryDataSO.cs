using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew;
using WereAllGonnaDieAnywayNew.InventorySystem;

[CreateAssetMenu(fileName = "New HarvestPoint Factory Data SO", menuName = "Factory/New HarvestPoint Factory Data SO")]
public class HarvestPointFactoryDataSO : ScriptableObject
{
    public string LocationName;
    public Vector3 LocationCoordinates;
    public float Crowdedness;

    public ResourceSpawnRateSO SpawnRateData;
    public AbundanceDataSO AbundanceData;

    public WereAllGonnaDieAnywayNew.InventorySystem.Inventory HarvestPointInventory;

    public void UpdateCrowdedness(HarvestPointFactoryDataSO updatedLocation, float newValue)
    {
        if(updatedLocation == this)
            Crowdedness = newValue;
    }

    public void UpdateAbundance(HarvestPointFactoryDataSO updatedHarvestPoint, out bool hasItem)
    {
        hasItem = false;

        if (updatedHarvestPoint != this)
            return;

        List<ResourceDataSO> resourceList = new List<ResourceDataSO>(AbundanceData.ResourceList);
        List<ItemFactoryData> itemsInBag = new List<ItemFactoryData>(HarvestPointInventory.ItemsInBag);

        for (int i = 0; i < itemsInBag.Count; i++)
        {
            for (int j = 0; j < itemsInBag.Count; j++)
            {
                if (itemsInBag[j].ItemName == resourceList[i].Item.ItemName)
                {
                    HarvestPointInventory.db.ItemDataMasterList.TryGetValue(resourceList[i].Item.ItemName, out SourceDataItemSO itemSO);
                    float currentDaysWorth = (float)itemsInBag[j].quantity / (float)itemSO.daysWorthRatio;
                    //Debug.Log("currentDaysWorth = " + currentDaysWorth + "// quantity: " + itemsInBag[j].quantity + "days worth ratio: " + itemSO.daysWorthRatio);
                    resourceList[i].Item.quantity = itemsInBag[i].quantity;
                    resourceList[i].AbundanceValue = currentDaysWorth * 20;
                }
            }
        }
    }
}
