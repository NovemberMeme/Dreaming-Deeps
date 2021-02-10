using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace cc.datatype
{
    [CreateAssetMenu(menuName = "Data Type/String")]
    public class StringVar : CustomDataType<string>
    {
        [TextArea]
        public new string value;

        public StringVar (string _value)
        {
            value = _value;
        }

        public StringVar()
        {
            value = " ";
        }


        public void SetValue(string _value)
        {
            value = _value;
        }


        public void Clear()
        {
            value = " ";
        }
    }

}
