using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    public static class DelegateManager 
    {
        public delegate void UpdateHarvestPointItems(HarvestPointFactoryDataSO updatedHarvestPoint, out bool hasItem);
        public static UpdateHarvestPointItems updateHarvestPointItems;

        public delegate void UpdateCrowdedness(HarvestPointFactoryDataSO updatedLocation, float newValue);
        public static UpdateCrowdedness updateCrowdedness;

        public delegate void AttemptCreatureInteraction(out Vector2 playerPos);
        public static AttemptCreatureInteraction attemptCreatureInteraction;

        public delegate void BeginCreatureInteraction(Creature otherCreature);
        public static BeginCreatureInteraction beginCreatureInteraction;

        public delegate void EndCreatureInteraction();
        public static EndCreatureInteraction endCreatureInteraction;

        //public delegate void BroadcastPlayer(Player player);
        //public static BroadcastPlayer broadcastPlayer;

        public delegate void AttemptScavenge(HarvestPointMonobehaviour hpmb);
        public static AttemptScavenge attemptScavenge;
    }
}