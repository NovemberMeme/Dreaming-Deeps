  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace WereAllGonnaDieAnywayNew.InventorySystem
{
    /// <summary>
    ///  NOTE: refactor add and remove items 
    /// </summary>


    public class Inventory : MonoBehaviour
    {
        [Tooltip("Game Data Base Goes Here")]
        public GameDatabaseSO db;

        public List<ItemFactoryData> ItemsInBag = new List<ItemFactoryData>(); // we cant delete this becase were passing the string and the float to the database
        Dictionary<string, UsableItem> BagLookUp = new Dictionary<string, UsableItem>();

        EntityStatHandler owner;
        EntityStatHandler target;

        //dont need this yet
        //public delegate void UseItemDelegate(Actor Target);
        //public UseItemDelegate UseItem;


        #region Monobehaviour Callbacks


        private void Start()
        {
            RefreshInventory();
            target = TryGetComponent<Creature>(out Creature c)? c.Stats : null;

        }

        #endregion

        #region Custom Callbacks

        /// <summary>
        /// Sets the target where an item is used at
        /// </summary>
        /// <param name="go"></param>
        public void SetTarget(GameObject go)
        {
            if (go.TryGetComponent<Creature>(out Creature target))
            {
                this.target = target.Stats;
            }
        }

        /// <summary>
        /// If a target hasn't been set the item would be used by the owner
        /// </summary>
        /// <param name="itemName"></param>
        public void UseThisItem(string itemName)
        {
            if (BagLookUp.TryGetValue(itemName, out UsableItem itemToUse))
            {
                itemToUse.SetTarget(target);
                itemToUse.OnUse();

                if (itemToUse.Quantity <= 0)
                {
                   BagLookUp.Remove(itemName);                     
                  
                }
            }
            else
            {
                Debug.LogWarning("ITEM DOES NOT EXIST");
            }

        }

        /// <summary>
        /// Transfers the item to another inventory regardless of quantity
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="lootedItem"></param>
        /// <returns></returns>
        public bool LootThisItem(string itemName, out UsableItem lootedItem)
        {
            if (BagLookUp.TryGetValue(itemName, out lootedItem))
            {
                BagLookUp.Remove(itemName);
                removeFromBag(itemName);
               
                return true;
            }
            else
            {
                Debug.LogWarning("ITEM DOES NOT EXIST");
                return false;
            }
        }

        /// <summary>
        /// Transfers Item to another inventory
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="amount"></param>
        /// <param name="targetItem"></param>
        /// <returns></returns>
        public bool TransferItem(string itemName,int amount, out UsableItem targetItem)
        {
            if (BagLookUp.TryGetValue(itemName, out UsableItem item))
            {
                if (item.Quantity <= 0 || item.Quantity < amount)
                {
                    targetItem = item;
                    return false;
                }

                item.Quantity -= amount;
                removeFromBag(item.ItemName, amount);
                targetItem = item;
                targetItem.Quantity = amount;

                return true;
            }
            else
            {
                Debug.LogWarning("ITEM DOES NOT EXIST");
                targetItem = item;
                return false;
            }
        }


        /// <summary>
        /// Transfers Item to another inventory
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="targetItem"></param>
        /// <returns></returns>
        public bool TransferItem(string itemName, out UsableItem targetItem)
        {
            if (BagLookUp.TryGetValue(itemName, out UsableItem item))
            {
                if (item.Quantity <= 0)
                {
                    targetItem = null;
                    item.Quantity = 0;
                    //removeFromBag(item.ItemName);
                    return false;
                }

                item.Quantity -= 1;
                //removeFromBag(item.ItemName, 1);
                targetItem = item;
                targetItem.Quantity = 1;
                return true;
            }
            else
            {
                Debug.LogWarning("ITEM DOES NOT EXIST");
                targetItem = item;
                return false;
            }
        }
        


        /// <summary>
        /// Updates usable items in inventory
        /// </summary>
        public void RefreshInventory()
        {                
            BagLookUp.Clear();

            foreach (ItemFactoryData temp in ItemsInBag)
            {
                SourceDataItemSO tmp;

                if (db.ItemDataMasterList.TryGetValue(temp.ItemName, out tmp))
                {
                    UsableItem itemToAdd = CreateDataListEntryItem(tmp, temp);

                    if (!BagLookUp.TryGetValue(temp.ItemName, out UsableItem item))
                    {
                        BagLookUp.Add(temp.ItemName, itemToAdd);
                      
                    }
                    else
                    {
                        item.Quantity += itemToAdd.Quantity;
                    }


                }
            }
                    
        }

 


        UsableItem CreateDataListEntryItem(SourceDataItemSO source, ItemFactoryData template)
        {
            ITEMTYPE type = source.type;
            UsableItem tmp;
            switch (type)
            {
                case ITEMTYPE.FOOD:
                    tmp = new ConsumableItem(template, source);
                    //tmp.SetTarget(owner);
                    return tmp;
                // break;
                case ITEMTYPE.CLEANING_MATERIAL:
                    tmp = new ConsumableItem(template, source);
                    //tmp.SetTarget(owner);
                    return tmp;
                case ITEMTYPE.PROTECTIVE_EQUIPMENT:
                    tmp = new EquipmentItem(template, source);
                    //tmp.SetTarget(owner);
                    return tmp;
                    // break;


            }
            Debug.LogError("type not found, please check: " + template.ItemName);
            return null;
        }


        #endregion

        /// <summary>
        /// Get a Usable Item and its quantity
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public UsableItem FindItemQuantity(string _name, out int quantity)
        {

            if (BagLookUp.TryGetValue(_name, out UsableItem item))
            {
                quantity = item.Quantity;

                return item;
            }
            else
            {
                quantity = 0;
               //f Debug.LogWarning("Item does not exist");
                return null;
            }
        }

      


        /// <summary>
        /// Checks if Item is in inventory
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public bool HasItem(string _name)
        {            
          //  return (BagLookUp.TryGetValue(_name, out ItemBase item) && item.Quantity > 0);      
            foreach(ItemFactoryData item in ItemsInBag)
            {
                if (item.ItemName == _name)
                    return true;
            }
            return false;   
                          
        }

        public bool HasItem(string _name, out int qty)
        {            
            //  return (BagLookUp.TryGetValue(_name, out ItemBase item) && item.Quantity > 0);      
            foreach (ItemFactoryData item in ItemsInBag)
            {
                if (item.ItemName == _name)
                {
                    qty = item.quantity;
                    return true;
                }
                    
            }
            qty = 0;
            return false;

        }


        public void AddItem(UsableItem itemToAdd)
        {

            if (BagLookUp.TryGetValue(itemToAdd.ItemName, out UsableItem item))
            {
                item.Quantity += itemToAdd.Quantity;
                addToBag(itemToAdd.ItemName, itemToAdd.Quantity);
               // ItemsInBag.Add(new ItemDataTemplate(itemToAdd.ItemName, itemToAdd.Quantity));
            }
            else
            {
                BagLookUp.Add(itemToAdd.ItemName, itemToAdd);
                addToBag(itemToAdd.ItemName,itemToAdd.Quantity);

            }

            refreshBag(new ItemFactoryData(itemToAdd.ItemName,itemToAdd.Quantity));
        }

        /// <summary>
        /// Call this to add a new item to 
        /// an inventory
        /// </summary>
        /// <param name="itemToAdd"></param>
        public void AddItemToBag(ItemFactoryData itemToAdd)
        {
           foreach(ItemFactoryData itm in ItemsInBag)
            {
                if(itm.ItemName == itemToAdd.ItemName)
                {
                    itm.quantity += 1;
                    return;
                }
            }
            itemToAdd.quantity = 1;
            ItemsInBag.Add(itemToAdd);
            refreshBag(itemToAdd);
            UpdateAbundanceData();
        }

        public void AddItemToBag(ItemFactoryData itemToAdd,int qty)
        {
            foreach (ItemFactoryData itm in ItemsInBag)
            {
                if (itm.ItemName == itemToAdd.ItemName)
                {
                    itm.quantity += qty;
                    return;
                }
            }
            itemToAdd.quantity = qty;
            ItemsInBag.Add(itemToAdd);
            refreshBag(itemToAdd);

        }

        public void RemoveItem(UsableItem itemToRemove)
        {

            if (BagLookUp.TryGetValue(itemToRemove.ItemName, out UsableItem item))
            {
                removeFromBag(itemToRemove.ItemName);              
            }
            
        }

        public void ClearAll()
        {
            ItemsInBag.Clear();
            BagLookUp.Clear();
        }

        public bool TransferItemData(string itemName, int qty, out ItemFactoryData itemToTransfer)
        {
            foreach (ItemFactoryData itm in ItemsInBag)
            {
                if (itm.ItemName == itemName)
                {
                    if (itm.quantity < qty)
                    {
                        itemToTransfer = null;                        
                        //removeFromBag(item.ItemName);
                        return false;
                    }

                    itm.quantity -= qty;
                    //removeFromBag(item.ItemName, 1);
                    itemToTransfer = new ItemFactoryData(itm.ItemName, qty);                   
                    return true;
                }
            }
            itemToTransfer = null;
            return false;
        }


        #region Private Methods
        void addToBag(string name, int qty)
        {
            foreach (ItemFactoryData item in ItemsInBag)
            {
                if (item.ItemName == name)
                {
                    item.quantity += qty;
                    return;
                }
            }
            ItemsInBag.Add(new ItemFactoryData(name, qty));

        }

       

        void removeFromBag(string name, int qty)
        {
            int i;
            for ( i = 0 ; i < ItemsInBag.Count; i++)
            {
                if (ItemsInBag[i].ItemName == name)
                {
                    if (ItemsInBag[i].quantity >= qty)
                    {
                        ItemsInBag[i].quantity -= qty;
                        return;
                    }
                    else
                    {
                        ItemsInBag[i].quantity = 0;
                        
                    }

                }
            }

            ItemsInBag.RemoveAt(i);
            BagLookUp.Remove(name);
        }

        void removeFromBag(string name)
        {
            int i;
            for (i = 0; i < ItemsInBag.Count; i++)
            {
                if (ItemsInBag[i].ItemName == name)
                {
                    break;
                }
            }

            ItemsInBag.RemoveAt(i);
            BagLookUp.Remove(name);
        }


        void refreshBag(ItemFactoryData temp)
        {
            SourceDataItemSO tmp;

            if (db.ItemDataMasterList.TryGetValue(temp.ItemName, out tmp))
            {
                UsableItem itemToAdd = CreateDataListEntryItem(tmp, temp);

                if (!BagLookUp.TryGetValue(temp.ItemName, out UsableItem item))
                {
                    BagLookUp.Add(temp.ItemName, itemToAdd);

                }
                else
                {
                    item.Quantity += itemToAdd.Quantity;
                }

            }
        }


        #endregion

        #region Custom Functions

        public AbundanceDataSO myAbundanceData;

        private void UpdateAbundanceData()
        {
            if (myAbundanceData == null)
                return;

            List<ResourceDataSO> resourceList = new List<ResourceDataSO>(myAbundanceData.ResourceList);
            List<ItemFactoryData> itemsInBag = new List<ItemFactoryData>(ItemsInBag);

            for (int i = 0; i < itemsInBag.Count; i++)
            {
                for (int j = 0; j < itemsInBag.Count; j++)
                {
                    if (itemsInBag[j].ItemName == resourceList[i].Item.ItemName)
                    {
                        db.ItemDataMasterList.TryGetValue(resourceList[i].Item.ItemName, out SourceDataItemSO itemSO);
                        float currentDaysWorth = (float)itemsInBag[j].quantity / (float)itemSO.daysWorthRatio;
                        //Debug.Log("currentDaysWorth = " + currentDaysWorth + "// quantity: " + itemsInBag[j].quantity + "days worth ratio: " + itemSO.daysWorthRatio);
                        resourceList[i].Item.quantity = itemsInBag[i].quantity;
                        resourceList[i].AbundanceValue = currentDaysWorth * 20;
                    }
                }
            }
        }

        #endregion


        // data needs to get refreshed on editor ,need to fix this eventually

        [RuntimeInitializeOnLoadMethod]
        [ContextMenu("REFRESH_DATA")]
        public void REFRESH_DATA()
        {
            db.Initialize();
        }

        private void OnValidate()
        {
            REFRESH_DATA();
        }


    }



}
