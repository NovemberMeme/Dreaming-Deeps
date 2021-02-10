
using System.Collections.Generic;
using UnityEngine;

public static class ExamplePropertyDrawersHelper
{

    static DataTest _source;
    public static void setData (DataTest source)
    {
        _source = source;
    }

    public static string[] GetDataFromSource()
    {
        return _source.names.ToArray();
    }
    public static string[] methodExample()
    {
        var temp = new List<string>();
        temp.Add("Example");
        temp.Add("Second");
        return temp.ToArray();
    }

}