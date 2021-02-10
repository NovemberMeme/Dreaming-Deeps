using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    [CreateAssetMenu(fileName = "New Character Info Template", menuName = "Info/New Character Info Template")]
    public class CharacterInfoTemplate : ScriptableObject
    {
        public List<Stat> RelevantActorStats = new List<Stat>();
    }
}