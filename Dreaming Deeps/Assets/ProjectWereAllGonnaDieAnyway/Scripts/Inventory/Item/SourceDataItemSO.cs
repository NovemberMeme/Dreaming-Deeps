using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew.InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/ItemTemplate", order = 1)]

    public class SourceDataItemSO : ScriptableObject
    {
        public ITEMTYPE type = ITEMTYPE.FOOD;
        public Sprite icon;
        public float weight;
        public int daysWorthRatio;
        public int resourcePriority;

        public List<EntityStatModifier> StatEffectList = new List<EntityStatModifier>();

       
    }

   
}
