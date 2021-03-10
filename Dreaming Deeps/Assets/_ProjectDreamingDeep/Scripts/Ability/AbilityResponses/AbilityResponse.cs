using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Ability Response", menuName = "Abilities/New Ability Response")]
    public class AbilityResponse : ScriptableObject
    {
        public List<AbilityResponse> ExtraAbilityResponses = new List<AbilityResponse>();

        public virtual void RespondToAbility(AbilityData _abilityData)
        {
            if (!_abilityData.IsSubEffect)
                CustomResponse(_abilityData);
            else
                _abilityData.Target.GetDamaged(_abilityData);

            for (int i = 0; i < ExtraAbilityResponses.Count; i++)
            {
                ExtraAbilityResponses[i].RespondToAbility(_abilityData);
            }
        }

        public virtual void CustomResponse(AbilityData _abilityData)
        {

        }
    }
}