using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Party", menuName = "Adventure/New Party")]
    public class Party : ScriptableObject
    {
        public List<PartyCharacter> PartyCharacterList = new List<PartyCharacter>();

        public PartyCharacter GetCharacterByIndex(int _charIndex)
        {
            if(_charIndex >= PartyCharacterList.Count)
            {
                _charIndex = PartyCharacterList.Count - 1;
            }

            return PartyCharacterList[_charIndex];
        }
    }
}