using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew;
using System;


[System.Serializable]
public class EntityStatHandler 
{   
    public List<Stat> StatList = new List<Stat>();
    //[HideInInspector]
    //public ActorStatList Stats; // FOR LOGICAL PURPOSES
  //  public Dictionary<STAT_TYPE, ActorStat> StatsDict = new Dictionary<STAT_TYPE, ActorStat>();



    public EntityStatHandler()
    {

    }

   
    public EntityStatHandler(List<Stat> statList)
    {
        StatList = TOOLS.CloneObject<List<Stat>>(statList);
    }
    public EntityStatHandler(Stat[] _statList)
    {
        StatList = new List<Stat>(_statList);
    }


    public void AddStat(STAT_TYPE targetStatType, float addValue, out bool addSuccessful)
    {
        for (int i = 0; i < StatList.Count; i++)
        {
            if (StatList[i].statType == targetStatType)
            {
                StatList[i].BaseValue += addValue;
                StatList[i].CurrentValue += addValue;

                addSuccessful = true;
            }
        }

        addSuccessful = false;
    }

    public void AddStat(STAT_TYPE targetStatType, float addValue)
    {
        for (int i = 0; i < StatList.Count; i++)
        {
            if (StatList[i].statType == targetStatType)
            {
                StatList[i].BaseValue += addValue;
                StatList[i].CurrentValue += addValue;

               // addSuccessful = true;
            }
        }

       // addSuccessful = false;
    }

    public void SetStat(STAT_TYPE targetStatType, float newValue, out bool setSuccessful)
    {
        setSuccessful = false;

        for (int i = 0; i < StatList.Count; i++)
        {
            if (StatList[i].statType == targetStatType)
            {               

                StatList[i].BaseValue = newValue;
                StatList[i].CurrentValue = newValue;

                Mathf.Clamp(StatList[i].BaseValue, 0, 100);
                Mathf.Clamp(StatList[i].CurrentValue, 0, 100);

                setSuccessful = true;
            }
        }
    }

    public void SetStat(STAT_TYPE targetStatType, float newValue)
    {
        for (int i = 0; i < StatList.Count; i++)
        {
            if (StatList[i].statType == targetStatType)
            {

                StatList[i].BaseValue = newValue;
                StatList[i].CurrentValue = newValue;

                Mathf.Clamp(StatList[i].BaseValue, 0, 100);
                Mathf.Clamp(StatList[i].CurrentValue, 0, 100);


            }
        }
    }

    /// <summary>
    /// Sets only the Current Value and not the Base Value
    /// </summary>
    /// <param name="targetStatType"></param>
    /// <param name="newValue"></param>

    public void SetCurrentStat(STAT_TYPE targetStatType, float newValue)
    {
        for (int i = 0; i < StatList.Count; i++)
        {
            if (StatList[i].statType == targetStatType)
            {

                StatList[i].CurrentValue = newValue;
                Mathf.Clamp(StatList[i].CurrentValue, 0, 100);


            }
        }
    }

    /// <summary>
    /// Returns Stat from Entity statList, returns null if stat isn't found
    /// </summary>
    /// <param name="targetStatType"></param>
    /// <returns></returns>

    public Stat FindStat(STAT_TYPE targetStatType)
    {
        for (int i = 0; i < StatList.Count; i++)
        {
            if (StatList[i].statType == targetStatType)
            {
                return StatList[i];
            }
        }

        return null;
    }

    /// <summary>
    /// Finds the Current Value specifically
    /// </summary>
    /// <param name="targetStatType"></param>
    /// <param name="value"></param>
    /// <returns></returns>

    public Stat FindCurrentStat(STAT_TYPE targetStatType, out float value)
    {
        for (int i = 0; i < StatList.Count; i++)
        {
            if (StatList[i].statType == targetStatType)
            {
                value = StatList[i].CurrentValue;
                return StatList[i];
            }
        }
        value = -1f;
        return null;
    }

    /// <summary>
    /// Finds the Base Value specifically
    /// </summary>
    /// <param name="targetStatType"></param>
    /// <param name="value"></param>
    /// <returns></returns>

    public Stat FindBaseStat(STAT_TYPE targetStatType, out float value)
    {
        for (int i = 0; i < StatList.Count; i++)
        {
            if (StatList[i].statType == targetStatType)
            {
                value = StatList[i].BaseValue;
                return StatList[i];
            }
        }
        value = -1f;
        return null;
    }

    /// <summary>
    /// Checks if stat exists in entity
    /// </summary>
    /// <param name="targetStatType"></param>
    /// <returns></returns>
    public bool HasStat(STAT_TYPE targetStatType)
    {
        for (int i = 0; i < StatList.Count; i++)
        {
            if (StatList[i].statType == targetStatType)
            {
                return true;
            }
        }

        return false;
    }

  

    
    public void createAllStats()
    {
        int statCount = Enum.GetValues(typeof(STAT_TYPE)).Length;

        for (int i = 0; i < statCount; i++)
        {
            STAT_TYPE _type = (STAT_TYPE)i;
            StatList.Add(new Stat(_type, 10));
        }

    }

}
