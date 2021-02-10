using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    [CreateAssetMenu(fileName = "New Object Info Template", menuName = "Info/New Object Info Template")]
    public class ObjectInfoTemplate : ScriptableObject
    {
        public List<Stat> RelevantObjectStats = new List<Stat>();
    }
}