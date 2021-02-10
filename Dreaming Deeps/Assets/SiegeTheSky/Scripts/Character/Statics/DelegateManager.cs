using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SiegeTheSky
{
    public static class DelegateManager
    {
        // Hover Info Panel
        public static GameObject hoveredObject;
        public static GameObject infoPanel;

        //// Tower Poolers: 

        //public static ObjectPooler combatTowerPooler;
        //public static ObjectPooler distributorPooler;
        //public static ObjectPooler harvesterPooler;
        //public static ObjectPooler powerplantPooler;

        //// Projectile Poolers: 

        //public static ObjectPooler bulletPooler;
        //public static ObjectPooler meteorPooler;
        //public static ObjectPooler fireballPooler;

        //// Enemy Poolers: 

        //public static ObjectPooler gruntPooler;
        //public static ObjectPooler shooterPooler;
        //public static ObjectPooler suicideBomberPooler;
        //public static ObjectPooler tankPooler;
        //public static ObjectPooler casterPooler;

        //// Material Poolers: 

        //public static ObjectPooler oxynditePooler;
        //public static ObjectPooler daltinonPooler;
        //public static ObjectPooler stravinumPooler;

        //// Module Poolers: 

        //public static ObjectPooler attackModulePooler;
        //public static ObjectPooler blockModulePooler;
        //public static ObjectPooler tauntModulePooler;
        //public static ObjectPooler sniperModulePooler;
        //public static ObjectPooler blasterModulePooler;

        public delegate void SpawnFromObjectPooler(GameObject poolabledObject, Vector3 startPosition);
        public static SpawnFromObjectPooler spawnFromObjectPooler;

        // Marker

        public static GameObject marker;

        public static List<GameObject> connectedTowers;
        public static ThingRuntimeSet allPowerPlants;

        public static Transform currentHoverSelection;

        //public static PowerPlant powerPlant;
        public static float currentPower;
        public static float maxPower;

        //public static Character currentTargetedEnemy;
        //public static Character playerCharacter;

        public delegate void StopHover();
        public static StopHover stopHover;

        public delegate void DeselectAll();
        public static DeselectAll deselectAll;

        //public delegate void BroadcastAction(Action action);
        //public static BroadcastAction broadcastAction;

        //public delegate void GetHit(Action action);
        //public static GetHit getHit;

        static DelegateManager()
        {
            connectedTowers = new List<GameObject>();
        }

        //public static bool AttemptUseEnergy(float energyCost, GameObject user)
        //{
        //    if (!connectedTowers.Contains(user))
        //    {
        //        Debug.Log("Not Connected!");
        //        return false;
        //    }

        //    if (currentPower < energyCost)
        //    {
        //        Debug.Log("Not Enough Energy");
        //        return false;
        //    }

        //    currentPower -= energyCost;

        //    return true;
        //}

        //public static void AttemptGainEnergy(float energyGain, GameObject gainer)
        //{
        //    if (!connectedTowers.Contains(gainer))
        //    {
        //        Debug.Log("Not Connected!");
        //        return;
        //    }

        //    if ((currentPower + energyGain) > maxPower)
        //    {
        //        currentPower = maxPower;
        //    }
        //    else
        //        currentPower += energyGain;
        //}

        //public static void CalculatePower()
        //{
        //    float newMaxPower = 0;

        //    for (int i = 0; i < allPowerPlants.Items.Count; i++)
        //    {
        //        GameObject currentPowerPlantObject = allPowerPlants.Items[i].gameObject;

        //        if (connectedTowers.Contains(currentPowerPlantObject))
        //        {
        //            PowerPlant currentPowerPlant = currentPowerPlantObject.GetComponent<PowerPlant>();

        //            newMaxPower += currentPowerPlant.MaxPower;
        //        }
        //    }

        //    if (maxPower != newMaxPower)
        //        maxPower = newMaxPower;

        //    if (currentPower > maxPower)
        //        currentPower = maxPower;
        //}

        #region Detection

        public static List<Transform> GetObjectList(ThingRuntimeSet thingRuntimeSet, float detectionRadius, Vector3 origin)
        {
            if (thingRuntimeSet.Items.Count < 1)
                return null;

            List<Transform> things = new List<Transform>();

            for (int i = 0; i < thingRuntimeSet.Items.Count; i++)
            {
                Transform thing = thingRuntimeSet.Items[i].transform;

                if (thing == null)
                    continue;

                if (Vector3.Distance(origin, thing.transform.position) <= detectionRadius)
                    things.Add(thing);
            }

            if (things.Count > 0)
                return things;
            else
                return null;
        }

        public static Transform GetNearestObject(ThingRuntimeSet thingRuntimeSet, float detectionRadius, Vector3 origin)
        {
            if (thingRuntimeSet.Items.Count < 1)
                return null;

            List<Transform> things = new List<Transform>();

            for (int i = 0; i < thingRuntimeSet.Items.Count; i++)
            {
                Transform thing = thingRuntimeSet.Items[i].transform;

                if (thing == null)
                    continue;

                if (Vector3.Distance(origin, thing.transform.position) <= detectionRadius)
                    things.Add(thing);
            }

            things = things.OrderBy(x => Vector3.Distance(origin, x.transform.position)).ToList();

            if (things.Count > 0)
                return things[0];
            else
                return null;
        }

        #endregion

        #region Selection

        public static void OnHover(Transform hoverSelection)
        {
            IHoverable currentHoverSelection = hoverSelection.GetComponent<IHoverable>();

            if (currentHoverSelection != null)
            {
                currentHoverSelection.OnHover();
            }
        }

        public static void OnClickSelect(Transform clickSelection)
        {
            if (clickSelection == null)
                return;

            IClickable currentClickSelection = clickSelection.GetComponent<IClickable>();

            if (currentClickSelection != null)
            {
                currentClickSelection.Click();
            }
        }

        public static void StopClickSelect(Transform clickSelection)
        {
            if (clickSelection == null)
                return;

            IClickable currentClickSelection = clickSelection.GetComponent<IClickable>();

            if (currentClickSelection != null)
            {
                currentClickSelection.UnClickSelect();
            }
        }

        #endregion
    }
}