using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew;

namespace WereAllGonnaDieAnywayNew.InventorySystem
{
   // [System.Serializable]
    public class ConsumableItem : UsableItem
    {
        public ConsumableItem(ItemFactoryData template, SourceDataItemSO source)
        {
           // Debug.Log("consummable created");
            base.Initialize(template);
            base.SetSource(source);
        }


        public override void OnUse()
        {
            //base.OnUse();                      

            foreach (EntityStatModifier mod in source.StatEffectList)
            {
                for (int i = 0; i < target.StatList.Count; i++)
                {
                    if(target.StatList[i].statType == mod.targetStat)
                    {
                        target.StatList[i].Modifiers.Add(mod);
                        target.StatList[i].ApplyModifiers();
                        break;
                    }

                }
            }
          

            quantity -= 1;
            Debug.Log(itemName + " was used");
        }

        
    }
}
