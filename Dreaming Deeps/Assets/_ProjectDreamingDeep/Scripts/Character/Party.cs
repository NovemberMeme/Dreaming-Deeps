using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Party", menuName = "Adventure/New Party")]
    public class Party : ScriptableObject
    {
        public List<PartyCharacter> MyParty = new List<PartyCharacter>();

        public PartyCharacter GetCharacterByIndex(int _charIndex)
        {
            if(_charIndex >= MyParty.Count)
            {
                _charIndex = MyParty.Count - 1;
            }

            return MyParty[_charIndex];
        }
    }
}