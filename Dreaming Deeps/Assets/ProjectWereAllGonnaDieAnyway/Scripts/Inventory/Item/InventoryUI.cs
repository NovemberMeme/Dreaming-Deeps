using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew.InventorySystem;
using TMPro;
using UnityEngine.UI;


namespace WereAllGonnaDieAnywayNew
{

    /// <summary>
    /// create a diff script for loot ui dont be lazy 
    /// DON'T BE LAZY ajhfkAD FH;JKL 
    /// ADD TWEENING FOR HOT BAR
    /// uwu
    /// </summary>

    public class InventoryUI : MonoBehaviour
    {

        [SerializeField] GameObject ItembuttonPrefab;
        [SerializeField] InventorySystem.Inventory owner;
        [SerializeField] RectTransform content;

        private void OnEnable()
        {

            if (owner != null)
            {
                owner.RefreshInventory();
                PopulateInventory();

                //if(owner.TryGetComponent(out Actor actor) && actor.actorType == ACTOR_TYPE.PLAYER)
                //{
                //    DelegateManager.UpdatePlayerUI = PopulateInventory;
                //}
            }
            else
            {
                //DelegateManager.setCurrentInventoryObject = SetOwner;
            }                          
            
        }
                

        //public void SetOwner(InteractableObject targetObj)
        //{
        //    if (targetObj.TryGetComponent(out owner))
        //    {
        //        owner.RefreshInventory();
        //        PopulateLootInventory();
        //    }
        //    else
        //    {
        //        Debug.LogWarning("inventory not found for " + targetObj.name);
        //    }
        //}
        //public void SetOwner(ActorInventory _owner)
        //{
        //    owner = _owner;
        //    owner.RefreshInventory();
        //    PopulateInventory();
           
        //}
        public void PopulateInventory()
        {
            if (owner.ItemsInBag.Count <= 0)
            {
                Debug.Log("No Items in Bag");
                return;
            }
            if (content.childCount > 0 && owner.ItemsInBag.Count>0)
            {
                for (int i = 0; i < content.childCount; i++)
                {
                    if(owner.HasItem(owner.ItemsInBag[i].ItemName))
                    {
                        GameObject go = content.GetChild(i).gameObject;
                        go.name = owner.ItemsInBag[i].ItemName;
                        go.SetActive(true);
                        go.GetComponentInChildren<TextMeshProUGUI>().text = owner.ItemsInBag[i].ItemName + "\n  x" + owner.ItemsInBag[i].quantity;
                        go.GetComponent<Image>().sprite = owner.FindItemQuantity(owner.ItemsInBag[i].ItemName, out int qty).GetSprite();
                        go.GetComponent<Button>().onClick.RemoveAllListeners();
                        go.GetComponent<Button>().onClick.AddListener(() => UseItem(go.name));
                        go.GetComponent<Button>().onClick.AddListener(() => UpdateInventory(go.name));

                    }
                 
                }
                int lastIndex = content.childCount;

                for (int k = lastIndex; k < owner.ItemsInBag.Count; k++)
                {
                    GameObject go = Instantiate(ItembuttonPrefab, content);
                    go.name = owner.ItemsInBag[k].ItemName;
                    go.GetComponentInChildren<TextMeshProUGUI>().text = owner.ItemsInBag[k].ItemName + "\n  x" + owner.ItemsInBag[k].quantity;
                    go.GetComponent<Image>().sprite = owner.FindItemQuantity(owner.ItemsInBag[k].ItemName, out int qty).GetSprite();
                    go.GetComponent<Button>().onClick.RemoveAllListeners();
                    go.GetComponent<Button>().onClick.AddListener(() => UseItem(go.name));
                    go.GetComponent<Button>().onClick.AddListener(() => UpdateInventory(go.name));
                }

                return;
            }
           


            foreach (ItemFactoryData item in owner.ItemsInBag)
            {
                GameObject go = Instantiate(ItembuttonPrefab, content);
                go.name = item.ItemName;
                go.GetComponentInChildren<TextMeshProUGUI>().text = item.ItemName + "\n  x" + item.quantity;
                go.GetComponent<Image>().sprite = owner.FindItemQuantity(item.ItemName, out int qty).GetSprite();
                go.GetComponent<Button>().onClick.AddListener(() => UseItem(go.name));
                go.GetComponent<Button>().onClick.AddListener(() => UpdateInventory(go.name));
            }

        }
        //public void PopulateLootInventory()
        //{
        //    Debug.Log("Populating Loot UI");
        //    if (owner.ItemsInBag.Count <= 0)
        //    {
        //        Debug.Log("No Items to Loot");
        //        return;
        //    }
        //    if (content.childCount > 0)
        //    {
        //        for (int i = 0; i < content.childCount; i++)
        //        {
        //            GameObject go = content.GetChild(i).gameObject;
        //            go.SetActive(true);
        //            go.name = owner.ItemsInBag[i].ItemName;
        //            go.GetComponentInChildren<TextMeshProUGUI>().text = owner.ItemsInBag[i].ItemName + "\n  x" + owner.ItemsInBag[i].quantity;
        //            go.GetComponent<Image>().sprite = owner.FindItemQuantity(owner.ItemsInBag[i].ItemName, out int qty).GetSprite();
        //            go.GetComponent<Button>().onClick.AddListener(() => LootItem(go.name));
        //            go.GetComponent<Button>().onClick.AddListener(() => UpdateInventory(go.name));
        //        }
        //        int lastIndex = content.childCount;

        //        for (int k = lastIndex; k < owner.ItemsInBag.Count; k++)
        //        {
        //            GameObject go = Instantiate(ItembuttonPrefab, content);
        //            go.name = owner.ItemsInBag[k].ItemName;
        //            go.GetComponentInChildren<TextMeshProUGUI>().text = owner.ItemsInBag[k].ItemName + "\n  x" + owner.ItemsInBag[k].quantity;
        //            go.GetComponent<Image>().sprite = owner.FindItemQuantity(owner.ItemsInBag[k].ItemName, out int qty).GetSprite();
        //            go.GetComponent<Button>().onClick.AddListener(() => LootItem(go.name));
        //            go.GetComponent<Button>().onClick.AddListener(() => UpdateInventory(go.name));
        //        }

        //        return;
        //    }
        //    else
        //    {
        //        foreach (ItemDataTemplate item in owner.ItemsInBag)
        //        {
        //            GameObject go = Instantiate(ItembuttonPrefab, content);
        //            go.name = item.ItemName;
        //            go.GetComponentInChildren<TextMeshProUGUI>().text = item.ItemName + "\n  x" + item.quantity;
        //            go.GetComponent<Image>().sprite = owner.FindItemQuantity(item.ItemName, out int qty).GetSprite();
        //            go.GetComponent<Button>().onClick.AddListener(() => LootItem(go.name));
        //            go.GetComponent<Button>().onClick.AddListener(() => UpdateInventory(go.name));
        //        }
        //    }

        //}

        private void UpdateInventory(string itemname)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                if (content.GetChild(i).name == itemname)
                {
                    owner.FindItemQuantity(itemname, out int qty);
                    if (qty <= 0) content.GetChild(i).gameObject.SetActive(false);
                    content.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = itemname + "\n  x" + qty;
                }
            }
        }


        public void UseItem(string itemname)
        {
            owner.UseThisItem(itemname);          
        }
    

        //public void LootItem(string itemName)
        //{
           
        //    ActorInventory target;
        //    if ( owner.LootThisItem(itemName,out ItemBase lootedItem) && DelegateManager.player.TryGetComponent(out target))
        //    {

        //        target.AddItem(lootedItem);                
        //        UpdateInventory(itemName);
                
        //    }
        //    //DelegateManager.UpdatePlayerUI?.Invoke();
        //}

    }


}
