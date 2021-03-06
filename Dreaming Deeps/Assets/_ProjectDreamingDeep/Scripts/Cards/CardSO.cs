﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/New Card")]
    public class CardSO : ScriptableObject
    {
        public Aura MyPassiveAura;

        public EnergyData MyEnergies;

        public TileAbility MyTileAbility;
    }
}