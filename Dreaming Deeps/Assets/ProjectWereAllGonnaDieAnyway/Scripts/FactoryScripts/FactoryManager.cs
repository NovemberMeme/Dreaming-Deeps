using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WereAllGonnaDieAnywayNew;

/// <summary>
/// Create an instance of "factory" scripts here
/// 
/// </summary>
public class FactoryManager : MonoBehaviour
{
    [SerializeField] GameDatabaseSO GameData;

    public List<Transform> HarvestPointLocationTransforms = new List<Transform>();
    public CreatureFactory creatureFactory = new CreatureFactory();
    public HarvestPointFactory HpFactory;

    public List<IManufacture> SpawningOrder = new List<IManufacture>(); // which one gets created first

    public void Awake()
    {
        HpFactory = new HarvestPointFactory(HarvestPointLocationTransforms);
        HpFactory.AssignGameDataBase(GameData);
        creatureFactory.AssignGameDataBase(GameData);
        SpawningOrder.Add(creatureFactory);
        SpawningOrder.Add(HpFactory);
        RunManufactureScripts();
    }

    private void Start()
    {
       
    }

    void RunManufactureScripts()
    {
        for (int i = 0; i < SpawningOrder.Count; i++)
        {
            SpawningOrder[i].CreateObjects();
        }
    }

    [ContextMenu("Create a citizen")]
    public void createRandomCitizen()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(GameData);
        #endif
        creatureFactory.AssignGameDataBase(GameData);
       creatureFactory.CreateRandomCitizen();
             
    }
       

    [ContextMenu("Genocide")]
    public void ClearcitizenList()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(GameData);
#endif
        GameData.CitizensMasterList.Clear();
    }
}
