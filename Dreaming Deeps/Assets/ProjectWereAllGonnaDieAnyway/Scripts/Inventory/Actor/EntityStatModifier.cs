using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    [System.Serializable]

    public class EntityStatModifier
        
    {
        
        public STAT_TYPE targetStat;
        public Modifier modifier = Modifier.NONE;
        public float value;

        public EntityStatModifier(STAT_TYPE targetStat, Modifier modifier, float value)
        {
            this.targetStat = targetStat;
            this.modifier = modifier;
            this.value = value;
        }

        public EntityStatModifier GetModifier()
        {
            return this;
        }
        public void Apply(Stat stat)
        {

            switch (modifier)
            {
                case Modifier.NONE:
                    break;

                case Modifier.DECREASE:

                    stat.CurrentValue -= (value);                
                    break;

                case Modifier.INCREASE:
                    stat.CurrentValue += (value);
                    break;
                                   

                case Modifier.PERCENT_INCREASE:
                    stat.CurrentValue += (value * stat.CurrentValue);
                    break;

                case Modifier.PERCENT_DECREASE:
                    stat.CurrentValue -= (value * stat.CurrentValue);
                    break;

                case Modifier.PERCENT_SET:
                    stat.CurrentValue = stat.BaseValue * value;
                    break;

            }
        }

       
    }
    public enum Modifier
    {
        INCREASE,
        DECREASE,
       
        PERCENT_INCREASE,
       
        PERCENT_DECREASE,     

        PERCENT_SET,
        NONE
    }
}


