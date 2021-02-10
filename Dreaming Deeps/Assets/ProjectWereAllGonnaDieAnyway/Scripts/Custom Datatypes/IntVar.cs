using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cc.datatype
{
    [CreateAssetMenu(menuName = "Data Type/Int")]
    public class IntVar : CustomDataType<int>
    {
     

        public IntVar(int _value)
        {
            value = _value;
        }
        public IntVar()
        {
            value = 0;
        }


        public void SetValue(int _value)
        {
            value = _value;
        }

        public void Clear()
        {
            value = 0;
        }
    }
}

