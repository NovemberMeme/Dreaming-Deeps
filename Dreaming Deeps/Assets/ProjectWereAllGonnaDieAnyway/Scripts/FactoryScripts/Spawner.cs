using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew;

/// <summary>
/// Factory Managers Base Class
/// </summary>
public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected GameDatabaseSO GameData;
    [SerializeField] protected List<IManufacture> SpawningOrder = new List<IManufacture>(); // which one gets created first
    [SerializeField] protected CreatureFactory creatureFactory = new CreatureFactory();
    protected virtual void RunManufactureScripts()
    {
        for (int i = 0; i < SpawningOrder.Count; i++)
        {
            SpawningOrder[i].CreateObjects();
        }
    }

    [ContextMenu("Populate 5")]
    public void create5RandomCitizen()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(GameData);
#endif
        creatureFactory.AssignGameDataBase(GameData);
        for (int i = 0; i < 5; i++)
        {
            creatureFactory.CreateRandomCitizen();
        }

    }

    [ContextMenu("Populate 1")]
    public void createRandomCitizen()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(GameData);
    #endif
        creatureFactory.AssignGameDataBase(GameData);
        creatureFactory.CreateRandomCitizen();
    }
   // [ContextMenu("Genocide")]
    public virtual void ClearcitizenList()
    {
     #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(GameData);
     #endif
        GameData.CitizensMasterList.Clear();
    }
}
