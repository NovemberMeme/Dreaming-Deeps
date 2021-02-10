using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExampleBehavior : MonoBehaviour
{
    [SerializeField] DataTest source;
    
    [DropDownList("Cat", "Dog")] public string Animal;

    //using a method 
    [DropDownList(typeof(ExamplePropertyDrawersHelper), "methodExample")]
    public string methodExample;
    public List<sample> sampleList = new List<sample>();

    [ContextMenu("Set")]
    public void Set()
    {
        ExamplePropertyDrawersHelper.setData(source);
    }
}

[System.Serializable]
public class sample
{
    [DropDownList(typeof(ExamplePropertyDrawersHelper), "GetDataFromSource")]
    public string samp;
}

public class DropDownList : PropertyAttribute
{
    public delegate string[] GetStringList();
    public DropDownList(params string[] list)
    {
        List = list;
    }
    public DropDownList(Type type, string methodName)
    {
        var method = type.GetMethod(methodName);
        if (method != null)
        {
            List = method.Invoke(null, null) as string[];
        }
        else
        {
            Debug.LogError("NO SUCH METHOD " + methodName + " FOR " + type);
        }
    }
    public string[] List;
}