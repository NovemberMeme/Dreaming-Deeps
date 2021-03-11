using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    public class TargetType : ScriptableObject
    {
        public virtual PartyCharacter GetByTargetType(PartyCharacter _user)
        {
            return null;
        }
    }
}