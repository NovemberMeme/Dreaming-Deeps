using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public class TargetFrontMost : MonoBehaviour, ITargetType
    {
        public PartyCharacter GetByTargetType(Party _party)
        {
            return _party.GetCharacterByIndex(0);
        }
    }
}