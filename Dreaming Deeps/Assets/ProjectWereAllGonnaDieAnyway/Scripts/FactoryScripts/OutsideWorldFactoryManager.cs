using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WereAllGonnaDieAnywayNew;

/// <summary>
/// Create an instance of "factory" scripts here
/// 
/// </summary>
public class OutsideWorldFactoryManager : Spawner
{
    #region Harvest Point Stuff
    [Space]
    public List<Transform> HarvestPointLocationTransforms = new List<Transform>();         
    public HarvestPointFactory HpFactory;

    #endregion

    public void Awake()
    {
        HpFactory = new HarvestPointFactory(HarvestPointLocationTransforms);
        HpFactory.AssignGameDataBase(GameData);
        creatureFactory.AssignGameDataBase(GameData);

        SpawningOrder.Add(HpFactory);
        SpawningOrder.Add(creatureFactory);
        
        base.RunManufactureScripts();
    }

    private void Start()
    {
       
    }

   

 
}
