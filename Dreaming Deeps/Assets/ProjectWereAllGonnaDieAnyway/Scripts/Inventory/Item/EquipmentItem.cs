using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew.InventorySystem
{
  //  [System.Serializable]
    public class EquipmentItem : UsableItem
    {
        public EquipmentItem(ItemFactoryData template, SourceDataItemSO source)
        {
           // Debug.Log("equipment cerated");
            base.Initialize(template);
            base.SetSource(source);
        }
               

        public override void OnUse()
        {
            base.OnUse();
            

        }
    }
}
