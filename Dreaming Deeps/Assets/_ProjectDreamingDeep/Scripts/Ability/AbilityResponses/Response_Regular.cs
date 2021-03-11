using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Regular Response", menuName = "Abilities/New Regular Response")]
    public class Response_Regular : AbilityResponse
    {
        public override void CustomResponse(AbilityData _abilityData)
        {
            _abilityData.Target.GetDamaged(_abilityData);
        }
    }
}