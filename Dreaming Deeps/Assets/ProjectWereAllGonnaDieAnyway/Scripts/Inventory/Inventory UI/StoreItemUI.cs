using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew.InventorySystem;
using UnityEngine.UI;
using TMPro;
public class StoreItemUI : MonoBehaviour
{
    [SerializeField] GameObject ItembuttonPrefab;
    [SerializeField] WereAllGonnaDieAnywayNew.InventorySystem.Inventory owner;
    [SerializeField] RectTransform content;
    [SerializeField] HouseHoldDataBaseSO HouseHoldDB;
    

    private void OnEnable()
    {

        if (owner != null)
        {
            owner.RefreshInventory();
            PopulateLootInventory();
                       
        }
       

    }

    public void PopulateLootInventory()
    {
        Debug.Log("Populating Loot UI");
        if (owner.ItemsInBag.Count <= 0)
        {
            Debug.Log("No Items to Loot");
            return;
        }
        if (content.childCount > 0)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                GameObject go = content.GetChild(i).gameObject;
                go.SetActive(true);
                go.name = owner.ItemsInBag[i].ItemName;
                go.GetComponentInChildren<TextMeshProUGUI>().text = owner.ItemsInBag[i].ItemName + "\n  x" + owner.ItemsInBag[i].quantity;
                go.transform.GetChild(0).GetComponent<Image>().sprite = owner.FindItemQuantity(owner.ItemsInBag[i].ItemName, out int qty).GetSprite();
                go.GetComponent<Button>().onClick.AddListener(() => StoreThisItem(go.name));
              
            }
            int lastIndex = content.childCount;

            for (int k = lastIndex; k < owner.ItemsInBag.Count; k++)
            {
                GameObject go = Instantiate(ItembuttonPrefab, content);
                go.name = owner.ItemsInBag[k].ItemName;
                go.GetComponentInChildren<TextMeshProUGUI>().text = owner.ItemsInBag[k].ItemName + "\n  x" + owner.ItemsInBag[k].quantity;
                go.transform.GetChild(0).GetComponent<Image>().sprite = owner.FindItemQuantity(owner.ItemsInBag[k].ItemName, out int qty).GetSprite();
                go.GetComponent<Button>().onClick.AddListener(() => StoreThisItem(go.name));
            
            }

            return;
        }
        else
        {
            foreach (ItemFactoryData item in owner.ItemsInBag)
            {
                GameObject go = Instantiate(ItembuttonPrefab, content);
                go.name = item.ItemName;
                go.GetComponentInChildren<TextMeshProUGUI>().text = item.ItemName + "\n  x" + item.quantity;
                go.transform.GetChild(0).GetComponent<Image>().sprite = owner.FindItemQuantity(item.ItemName, out int qty).GetSprite();
                go.GetComponent<Button>().onClick.AddListener(() => StoreThisItem(go.name));
           
            }
        }

    }

    private void UpdateInventory(string itemname)
    {
        if (content.childCount == 0) return;

        for (int i = 0; i < content.childCount; i++)
        {
            if (content.GetChild(i).name == itemname)
            {
                owner.HasItem(itemname, out int qty);
                Debug.Log(itemname + " : " + qty);
                if (qty <= 0)
                {
                    content.GetChild(i).gameObject.SetActive(false);
                    return;
                }
                content.GetChild(i).transform.GetComponentInChildren<TextMeshProUGUI>().text = itemname + "\n  x" + qty;
            }
        }
    }


    public void UseItem(string itemname)
    {
        owner.UseThisItem(itemname);
    }


    public void StoreThisItem(string itemName)
    {       
       
        HouseHoldDB.AddItem(owner, 1, itemName);
        UpdateInventory(itemName);
      
        //update the other inventory?
    }

   
}
