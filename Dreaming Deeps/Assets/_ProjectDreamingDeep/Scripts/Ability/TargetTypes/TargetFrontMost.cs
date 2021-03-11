using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New TargetFrontMost", menuName = "Abilities/New TargetFrontMost")]
    public class TargetFrontMost : TargetType
    {
        public override PartyCharacter GetByTargetType(PartyCharacter _user)
        {
            if (DelegateController.getOpposingParty == null)
                return null;

            Party OpposingParty = DelegateController.getOpposingParty.Invoke(_user);

            for (int i = 0; i < OpposingParty.PartyCharacterList.Count; i++)
            {
                if (OpposingParty.PartyCharacterList[i].MyCombatStates.Contains(CombatState.Combat))
                    return OpposingParty.PartyCharacterList[i];
            }

            return null;

            //return _party.GetCharacterByIndex(0);
        }
    }
}