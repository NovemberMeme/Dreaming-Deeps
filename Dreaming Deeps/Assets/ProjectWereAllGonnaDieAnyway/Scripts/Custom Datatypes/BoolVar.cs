using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cc.datatype
{
    [CreateAssetMenu(menuName = "Data Type/Bool")]
    public class BoolVar : CustomDataType<bool>
    {

        public BoolVar(bool _value)
        {
            value = _value;
        }
        public BoolVar()
        {
            value = false;
        }

        public void SetValue(bool _value)
        {
            value = _value;
        }


        public void Clear()
        {
            value = false;
        }
    }
}