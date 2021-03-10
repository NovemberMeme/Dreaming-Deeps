using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CombatSystem combatSystem;

        private void OnEnable()
        {
            combatSystem.EnableCombatSystem();
        }

        private void OnDisable()
        {
            combatSystem.DisableCombatSystem();
        }
    }
}