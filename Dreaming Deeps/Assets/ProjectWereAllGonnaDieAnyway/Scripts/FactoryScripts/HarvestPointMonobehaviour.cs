﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using WereAllGonnaDieAnywayNew.InventorySystem;

namespace WereAllGonnaDieAnywayNew
{
    public class HarvestPointMonobehaviour : MonoBehaviour, IPointerClickHandler
    {
        public AbundanceDataSO HarvestPointAbundanceData;
        public ResourceSpawnRateSO SpawnRate;
        public Inventory HarvestInventory;

        public void InitializeHarvestPoint()
        {
            HarvestInventory = GetComponent<Inventory>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            AttemptScavenge();
        }

        public void AttemptScavenge()
        {

        }
    }
}