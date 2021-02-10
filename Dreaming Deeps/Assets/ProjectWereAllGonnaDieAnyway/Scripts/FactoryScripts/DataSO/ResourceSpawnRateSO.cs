using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew;
using WereAllGonnaDieAnywayNew.InventorySystem;

[CreateAssetMenu(fileName = "New Resource Spawn Rate SO", menuName = "Factory/New Resource Spawn Rate SO")]
public class ResourceSpawnRateSO : ScriptableObject
{
    public List<SpawnRateData> SpawnRateList = new List<SpawnRateData>();
}

[System.Serializable]
public class SpawnRateData
{
    [DropDownList(typeof(GameDatabaseSO), "GET_ITEM_LIST")]
    public string ItemName;
    public int SpawnRate;
}
