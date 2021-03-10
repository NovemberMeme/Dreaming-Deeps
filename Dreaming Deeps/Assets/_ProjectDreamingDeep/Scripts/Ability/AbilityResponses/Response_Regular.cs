using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Regular Ability Response", menuName = "Abilities/New Regular Ability Response")]
    public class Response_Regular : AbilityResponse
    {
        public override void CustomResponse(AbilityData _abilityData)
        {
            _abilityData.Target.GetDamaged(_abilityData);
        }
    }
}