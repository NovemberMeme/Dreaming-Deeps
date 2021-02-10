using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    public enum InfoType
    {
        CharacterInfo,
        LocationInfo,
        EventInfo,
        ObjectInfo,
        RuleInfo
    }

    public abstract class Info : ScriptableObject
    {
        public HumanizeType HumanizeTypes;

        public List<string> MyDescriptions = new List<string>();

        public abstract string CommunicateRandomInfo();
    }
}