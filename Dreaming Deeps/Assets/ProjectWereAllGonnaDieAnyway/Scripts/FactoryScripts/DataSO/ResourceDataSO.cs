using WereAllGonnaDieAnywayNew.InventorySystem;
using UnityEngine;

[System.Serializable]
public class ResourceDataSO
{   
    [SerializeField]
    private ItemFactoryData item;
    [SerializeField]
    private float abundanceValue;

    public ResourceDataSO(ItemFactoryData item, float value)
    {
        Item = item;
        AbundanceValue = value;

    }

    public float AbundanceValue { get => abundanceValue; set => this.abundanceValue = value; }
    public ItemFactoryData Item { get => item; set => this.item = value; }
}

