using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WereAllGonnaDieAnywayNew.InventorySystem
{
    public enum ITEMTYPE
    {
        FOOD,      
        PROTECTIVE_EQUIPMENT,
        CLEANING_MATERIAL
      
    }

   [System.Serializable]
    public abstract class UsableItem
    {
        protected int id;
        protected SourceDataItemSO source;
        protected ITEMTYPE type;
        protected int quantity;
        protected float weight;
        protected string itemName;
        protected EntityStatHandler target;
        public float Weight { get => weight; set => weight = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public int ID { get => id; set => id = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public SourceDataItemSO Source { get => source; set => source = value; }

        public Sprite GetSprite()
        {
            return source.icon;
        }
     
        public virtual void Initialize(ItemFactoryData template)
        {                       

            quantity = template.quantity;
            itemName = template.ItemName;
        }

        public virtual void SetSource(SourceDataItemSO _source)
        {
            source = _source;
            weight = source.weight;
        }

        public void SetTarget(EntityStatHandler _target)
        {
            target = _target;
        }


        public void SetID(int val)
        {
            id = val;
        }
        public float GetWeight()
        {
            return source.weight * quantity;
        }
        public virtual void OnUse()
        {
            return;
        }

        

    }

}
