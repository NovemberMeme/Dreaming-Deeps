using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew.InventorySystem;
using WereAllGonnaDieAnywayNew;

[CreateAssetMenu(fileName = "AbundanceDataSO", menuName = "Factory/New Abundance Data SO")]
public class AbundanceDataSO : ScriptableObject
{
    public List<ResourceDataSO> ResourceList = new List<ResourceDataSO>();
    //add daysworth somewere

    public ResourceDataSO FindResourceByName(string _resourceName)
    {
        for (int i = 0; i < ResourceList.Count; i++)
        {
            if(ResourceList[i].Item.ItemName == _resourceName)
            {
                return ResourceList[i];
            }
        }

        return null;
    }
}

