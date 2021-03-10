using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public static class DelegateController 
    {
        public delegate void Tick(int _tick);
        public static Tick tick;

        public delegate float GetTicksPerSecond();
        public static GetTicksPerSecond getTicksPerSecond;

        public delegate Party GetOpposingParty(PartyCharacter _partyChar);
        public static GetOpposingParty getOpposingParty;
    }
}