using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cc.datatype
{
    [CreateAssetMenu(menuName = "Data Type/Float")]
    public class FloatVar : CustomDataType<float>
    {
       

        public FloatVar(float _value)
        {
            value = _value;
        }
        public FloatVar()
        {
            value = 0;
        }

        public void SetValue(float _value)
        {
            value = _value;
        }


        public void Clear()
        {
            value = 0;
        }
    }
}