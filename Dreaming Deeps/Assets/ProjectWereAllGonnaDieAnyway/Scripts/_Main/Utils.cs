using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WereAllGonnaDieAnywayNew
{
    public static class Utils
    {
        #region Global Helper Functions

        public static float Remap(float outputMin, float outputMax, float inputMin, float inputMax, float input)
        {
            input = Mathf.Clamp(input, inputMin, inputMax);
            float percentage = (input - inputMin) / (inputMax - inputMin);
            float unscaledOutput = percentage * (outputMax - outputMin);
            return unscaledOutput + outputMin;
        }

        public static float CalculateWorkSpeed(float workSpeedStat, float IOWorkDifficulty)
        {
            return ((IOWorkDifficulty / workSpeedStat) * 10) + UnityEngine.Random.Range(-1, 3);
        }

        public static string BuildStringChain(List<SerializedDictionaryEntry_StringChain> stringChain)
        {
            string builtString = "";

            SerializedDictionaryEntry_StringChain tempStringChain = new SerializedDictionaryEntry_StringChain("temp", 0);

            for (int i = 0; i < stringChain.Count; i++)
            {
                for (int j = 0; j < stringChain.Count; j++)
                {
                    if (j < stringChain.Count - 1 &&
                        stringChain[j].Index > stringChain[j + 1].Index)
                    {
                        tempStringChain = stringChain[j];
                        stringChain[j] = stringChain[j + 1];
                        stringChain[j + 1] = tempStringChain;
                    }
                }
            }

            for (int i = 0; i < stringChain.Count; i++)
            {
                builtString += stringChain[i].StringText;
            }

            return builtString;
        }

        #endregion

        #region Infection Formulas

        // List of stats that affect Vulnerability

        //public static InfectionDataTemplate GlobalInfectionDataTemplate;

        //public static bool CalculateInfection(List<Creature> entities)
        //{
        //    // Get highest Infection Level among entities

        //    Stat highestInfectionLevel = GetHighestStat(entities, STAT_TYPE.infection_level, out Creature actorWithHighestStat);

        //    if (highestInfectionLevel.CurrentValue <= 0.1f)
        //        return false;

        //    for (int i = 0; i < entities.Count; i++)
        //    {
        //        if (entities[i] != actorWithHighestStat)
        //        {
        //            // Calculate Vulnerability

        //            float vulnerability = 0;

        //            for (int j = 0; j < GlobalInfectionDataTemplate.StatWeights.Count; j++)
        //            {
        //                vulnerability += entities[i].Stats.FindStat(
        //                    GlobalInfectionDataTemplate.StatWeights[j].ActorStat.statType).CurrentValue *
        //                    GlobalInfectionDataTemplate.StatWeights[j].Weight;
        //            }

        //            entities[i].Stats.SetStat(STAT_TYPE.vulnerability, vulnerability, out bool setSuccessful);

        //            // Execute Formula with parameters... (InfectionLevel, Vulnerability)

        //            float randomInfectionLevel = UnityEngine.Random.Range(1, highestInfectionLevel.CurrentValue);
        //            float infectionLevelToReceive = randomInfectionLevel * (vulnerability / 100);
        //            entities[i].Stats.AddStat(STAT_TYPE.infection_level, infectionLevelToReceive, out bool addSuccessful);
        //        }
        //    }

        //    return true;
        //}

        public static List<Stat> GetStats(List<Creature> actors, STAT_TYPE targetStatType)
        {
            List<Stat> targetStatList = new List<Stat>();

            for (int i = 0; i < actors.Count; i++)
            {
                targetStatList.Add(actors[i].Stats.FindStat(targetStatType));
            }

            return targetStatList;
        }

        public static Stat GetHighestStat(List<Stat> stats)
        {
            float highestStatValue = 0;
            Stat highestStat = null;

            for (int i = 0; i < stats.Count; i++)
            {
                if (stats[i].CurrentValue > highestStatValue)
                {
                    highestStatValue = stats[i].CurrentValue;
                    highestStat = stats[i];
                }
            }

            return highestStat;
        }

        public static Stat GetHighestStat(List<Creature> actors, STAT_TYPE targetStatType, out Creature actorWithHighestStat)
        {
            float highestStatValue = 0;
            Stat highestStat = null;
            Creature highestStatActor = null;

            // Make a Dictionary

            Dictionary<Creature, Stat> targetStatDictionary = new Dictionary<Creature, Stat>();

            for (int i = 0; i < actors.Count; i++)
            {
                targetStatDictionary.Add(actors[i], actors[i].Stats.FindStat(targetStatType));
            }

            foreach (Creature actor in targetStatDictionary.Keys)
            {
                if (actor.Stats.FindStat(targetStatType).CurrentValue > highestStatValue)
                {
                    highestStatValue = actor.Stats.FindStat(targetStatType).CurrentValue;
                    highestStat = actor.Stats.FindStat(targetStatType);
                    highestStatActor = actor;
                }
            }

            actorWithHighestStat = highestStatActor;

            return highestStat;
        }

        #endregion
    }
}