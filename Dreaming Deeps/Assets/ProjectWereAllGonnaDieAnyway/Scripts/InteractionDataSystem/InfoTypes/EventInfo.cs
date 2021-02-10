using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    [CreateAssetMenu(fileName = "New Event Info", menuName = "Info/New Event Info")]
    public class EventInfo : Info
    {
        public float DateOfOccurrence;
        public HarvestPointFactoryDataSO LocationOfOccurence;
        //public List<AI_Creature> AssociatedPeople;
        public List<WorldObject> AssociatedObjects;

        public override string CommunicateRandomInfo()
        {
            return "";
        }
    }
}
