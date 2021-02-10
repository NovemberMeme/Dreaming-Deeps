using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    public class Communicator : MonoBehaviour
    {
        public GlobalInfoProvider GlobalInfoProvider;

        public List<int> MyInfoList = new List<int>();

        public string CommunicateRandomInfo()
        {
            int myRandomInfoIndex = Random.Range(0, MyInfoList.Count);

            return GlobalInfoProvider.AllInfo[MyInfoList[myRandomInfoIndex]].CommunicateRandomInfo();
        }

        public void DisplayAllInfo()
        {
            for (int i = 0; i < MyInfoList.Count; i++)
            {
                GlobalInfoProvider.AllInfo[MyInfoList[i]].CommunicateRandomInfo();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                DisplayAllInfo();
            }
        }
    }
}