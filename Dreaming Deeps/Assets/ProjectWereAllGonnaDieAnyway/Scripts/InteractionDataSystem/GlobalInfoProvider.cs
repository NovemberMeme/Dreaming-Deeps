using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Global Info Provider", menuName = "Info/New Global Info Provider")]
    public class GlobalInfoProvider : ScriptableObject
    {
        [Header("Settings: ")]
        public int MaxRandomInfo = 5;
        public int MaxAntiRepeat = 10;

        [Header("Set Up: ")]
        public GameDatabaseSO DB;
        public List<Info> AllInfo = new List<Info>();
        public List<Communicator> currentCitizens = new List<Communicator>();

        public void DistributeTodaysInfo()
        {
            for (int i = 0; i < currentCitizens.Count; i++)
            {
                currentCitizens[i].GetComponent<Communicator>().MyInfoList = RandomInfoList(i);
            }
        }

        private List<int> RandomInfoList(int citizenIndex)
        {
            int randomInfoAmount = Random.Range(0, MaxRandomInfo);

            List<int> newInfoList = new List<int>();

            for (int i = 0; i < randomInfoAmount; i++)
            {
                int randomInfoIndex = Random.Range(0, AllInfo.Count);

                int repeats = 0;

                while (currentCitizens[citizenIndex].MyInfoList.Contains(randomInfoIndex) &&
                    repeats <= MaxAntiRepeat)
                {
                    randomInfoIndex = Random.Range(0, MaxRandomInfo);
                }

                newInfoList.Add(randomInfoIndex);
            }

            return newInfoList;
        }
    }
}