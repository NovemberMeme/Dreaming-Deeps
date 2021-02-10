using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cc.datatype
{
    [CreateAssetMenu(menuName = "Data Type/Vector3")]
    public class Vector3Var : CustomDataType<Vector3>
    {

        public Vector3Var(Vector3 _value)
        {
            value = _value;
        }
        public Vector3Var()
        {
            value = Vector3.zero;
        }

        public void SetValue(Vector3 _value)
        {
            value = _value;
        }


        public void Clear()
        {
            value = Vector3.zero;
        }
    }
}