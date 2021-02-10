namespace WereAllGonnaDieAnywayNew.InventorySystem
{
    [System.Serializable]
    public class ItemFactoryData
    {
        [DropDownList(typeof(GameDatabaseSO), "GET_ITEM_LIST")]
        public string ItemName;
        public int quantity;
        
        public ItemFactoryData(string itemName, int quantity)
        {
            ItemName = itemName;
            this.quantity = quantity;
        }
        public ItemFactoryData(UsableItem item)
        {
            ItemName = item.ItemName;
            this.quantity = item.Quantity;
        }
    }

}
