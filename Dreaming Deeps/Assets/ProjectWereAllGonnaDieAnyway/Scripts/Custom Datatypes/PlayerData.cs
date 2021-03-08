using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew;
using WereAllGonnaDieAnywayNew.InventorySystem;

[System.Serializable]
[CreateAssetMenu(menuName = "Data Type/Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    public List<ItemFactoryData> playerInventory = new List<ItemFactoryData>();
    [SerializeField]
    public List<Stat> playerStats = new List<Stat>();

    public void UpdateInventory(WereAllGonnaDieAnywayNew.InventorySystem.Inventory source)
    {
        playerInventory = TOOLS.CloneObject<List<ItemFactoryData>>(source.ItemsInBag);
    }
    
    public void UpdateStats(EntityStatHandler source )
    {
      playerStats = TOOLS.CloneObject<List<Stat>>(source.StatList);
    }
}
