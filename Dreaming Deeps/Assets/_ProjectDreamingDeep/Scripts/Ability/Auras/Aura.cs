﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Aura", menuName = "Abilities/New Aura")]
    public class Aura : ScriptableObject
    {
        public Passive MyPassiveEffect;

        public TileTemplateSO AffectedTiles;
    }
}