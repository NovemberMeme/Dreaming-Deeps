using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public enum EnergyType
    {
        Time,
        Space,
        Light,
        Void,
        Life,
        Death,
    }

    public class EnergyData
    {
        public int TimeEnergyAmount;
        public int SpaceEnergyAmount;
        public int LightEnergyAmount;
        public int VoidEnergyAmount;
        public int LifeEnergyAmount;
        public int DeathEnergyAmount;
    }
}