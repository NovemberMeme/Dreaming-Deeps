using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Dungeon", menuName = "Adventure/New Dungeon")]
    public class Dungeon : ScriptableObject
    {
        public List<Table_NightmareSpawn> MyNightmareSpawnTables = new List<Table_NightmareSpawn>();

        public List<Table_Resource> MyResourceTables = new List<Table_Resource>();
    }
}