using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    [CreateAssetMenu(fileName = "New Dialogue Data", menuName = "Info/New Dialogue Data")]
    public class DialogueData : ScriptableObject
    {
        public List<string> dialogueTextList = new List<string>();
    }
}