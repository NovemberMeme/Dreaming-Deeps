using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Combat System", menuName = "SO/New Combat System")]
    public class CombatSystem : ScriptableObject
    {
        public Party PlayerParty;
        public Party MonsterParty;

        public List<PartyCharacter> AllCharacters = new List<PartyCharacter>();

        public void EnableCombatSystem()
        {
            DelegateController.getOpposingParty += GetOpposingParty;
            EnableAllCharacters();
        }

        public void DisableCombatSystem()
        {
            DelegateController.getOpposingParty -= GetOpposingParty;
            DisableAllCharacters();
        }

        public void EnableAllCharacters()
        {
            for (int i = 0; i < AllCharacters.Count; i++)
            {
                AllCharacters[i].EnableCharacter();
            }
        }

        public void DisableAllCharacters()
        {
            for (int i = 0; i < AllCharacters.Count; i++)
            {
                AllCharacters[i].DisableCharacter();
            }
        }

        public Party GetOpposingParty(PartyCharacter _partyChar)
        {
            if (PlayerParty.PartyCharacterList.Contains(_partyChar))
                return MonsterParty;
            else
                return PlayerParty;
        }
    }
}