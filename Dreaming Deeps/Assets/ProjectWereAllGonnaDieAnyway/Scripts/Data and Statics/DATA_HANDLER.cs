using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew.InventorySystem
{// might have no use for this
    public static class DATA_HANDLER
    {
        static GameDatabaseSO _source; 
        public static void setData(GameDatabaseSO source)
        {
            _source = source;
            source.Initialize();
        }

        //public static string[] GET_ITEM_LIST()
        //{
        //    return _source.ItemNameMasterList.ToArray();
        //}

        public static bool TryGetSource(out GameDatabaseSO value)
        {

            if (_source != null)
            {
                value = _source;
                return true;
            }
            else
            {

                value = null;
                return false;
            }
           
        }

        //add all masterlist functions here
    }
}
