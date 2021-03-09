using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    public enum HumanizeEnum
    {
        LowHigh,
        GoodBad,
        NewPhrase
    }

    [CreateAssetMenu(fileName = "New Humanize Type", menuName = "Info/New Humanize Type")]
    public class HumanizeType : ScriptableObject
    {
        public List<STAT_TYPE> LowHighHumanizeType = new List<STAT_TYPE>();
        public List<STAT_TYPE> GoodBadHumanizeType = new List<STAT_TYPE>();
        public List<STAT_TYPE> NewPhraseHumanizeType = new List<STAT_TYPE>();

        public List<float> Ranges = new List<float>();

        public List<string> LowHighDescriptions = new List<string>();
        public List<string> GoodBadDescriptions = new List<string>();
        public List<string> HappinessDescriptions = new List<string>();
        public List<string> HungerDescriptions = new List<string>();
        public List<string> CleanlinessDescriptions = new List<string>();

        // Humanize Methods

        public string Humanize(float value, STAT_TYPE statType, out HumanizeEnum humanizeEnum)
        {
            for (int i = 0; i < GoodBadHumanizeType.Count; i++)
            {
                if (statType == GoodBadHumanizeType[i])
                {
                    humanizeEnum = HumanizeEnum.GoodBad;
                    return HumanizeGoodBad(value);
                }
            }

            for (int i = 0; i < NewPhraseHumanizeType.Count; i++)
            {
                if(statType == NewPhraseHumanizeType[i])
                {
                    humanizeEnum = HumanizeEnum.NewPhrase;
                    return HumanizeNewPhrase(value, statType);
                }
            }

            humanizeEnum = HumanizeEnum.LowHigh;
            return HumanizeLowHigh(value);
        }

        public string HumanizeLowHigh(float value)
        {
            for (int i = 0; i < Ranges.Count; i++)
            {
                if(value <= Ranges[i])
                {
                    return LowHighDescriptions[i];
                }
            }

            return "I can't compute LowHigh humanization...";
        }

        public string HumanizeGoodBad(float value)
        {
            for (int i = 0; i < Ranges.Count; i++)
            {
                if (value <= Ranges[i])
                {
                    return GoodBadDescriptions[i];
                }
            }

            return "I can't compute GoodBad humanization...";
        }

        public string HumanizeNewPhrase(float value, STAT_TYPE statType)
        {
            //switch (statType)
            //{
            //    case STAT_TYPE.happiness:
            //        for (int i = 0; i < Ranges.Count; i++)
            //        {
            //            if (value <= Ranges[i])
            //            {
            //                return HappinessDescriptions[i];
            //            }
            //        }
            //        break;
            //    case STAT_TYPE.hunger:
            //        for (int i = 0; i < Ranges.Count; i++)
            //        {
            //            if (value <= Ranges[i])
            //            {
            //                return HungerDescriptions[i];
            //            }
            //        }
            //        break;
            //    case STAT_TYPE.cleanliness:
            //        for (int i = 0; i < Ranges.Count; i++)
            //        {
            //            if (value <= Ranges[i])
            //            {
            //                return CleanlinessDescriptions[i];
            //            }
            //        }
            //        break;
            //}

            return "I can't compute NewPhrase humanization...";
        }
    }
}