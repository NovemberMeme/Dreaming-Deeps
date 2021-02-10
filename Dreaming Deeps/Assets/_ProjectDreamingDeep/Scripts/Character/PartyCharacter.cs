using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew;

namespace DreamingDeep
{
    public enum CombatState
    {
        Idle,
        Combat,
        Dead
    }

    public class PartyCharacter : MonoBehaviour
    {
        public Creature MyCreature;

        public CombatState MyCombatState = CombatState.Idle;
    }
}