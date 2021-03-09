using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamingDeep
{
    [CreateAssetMenu(fileName = "New Attack Ability Effect", menuName = "Abilities/New Attack Ability Effect")]
    public class AE_Attack : AbilityEffect
    {
        public override void ApplyMyCustomEffect(AbilityData _abilityData)
        {
            base.ApplyMyCustomEffect(_abilityData);

            // Attack pseudo code
            // Get damage stat of user
            // Get health stat of target
            // Subtract damage stat of user from health stat of target

            _abilityData.User.MyCreature.Stats.FindCurrentStat(WereAllGonnaDieAnywayNew.STAT_TYPE.Power, out float damageAmount);
            _abilityData.User.MyCreature.Stats.FindCurrentStat(WereAllGonnaDieAnywayNew.STAT_TYPE.MagicDamage, out float magicDamageAmount);

            _abilityData.PhysicalDamageAmount = damageAmount;
            _abilityData.MagicalDamageAmount = magicDamageAmount;

            _abilityData.Target.GetTargetedByAbilityResponse(_abilityData);
        }
    }
}