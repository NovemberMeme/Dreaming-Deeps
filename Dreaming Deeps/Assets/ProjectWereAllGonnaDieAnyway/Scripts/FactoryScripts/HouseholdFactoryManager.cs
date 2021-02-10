using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WereAllGonnaDieAnywayNew;
using WereAllGonnaDieAnywayNew.InventorySystem;

public class HouseholdFactoryManager : Spawner
{
    [SerializeField] HouseHoldDataBaseSO householdData;
    ResidentFactory residentFactory = new ResidentFactory();


    private void Awake()
    {
        residentFactory = new ResidentFactory(GameData, householdData);
        SpawningOrder.Add(residentFactory);

        RunManufactureScripts();
        
    }



    [ContextMenu("Add new Resident")]
    public void RecruitRandomCitizen()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(GameData);
        UnityEditor.EditorUtility.SetDirty(householdData);
#endif
        residentFactory.AssignHouseHoldData(householdData);
        residentFactory.AssignGameDatabase(householdData.gamedata);       
        residentFactory.addNewResident();
    }
    [ContextMenu("Clear Residents")]
    public void clearResidents()
    {
     #if UNITY_EDITOR

        UnityEditor.EditorUtility.SetDirty(GameData);
        UnityEditor.EditorUtility.SetDirty(householdData);

#endif
        residentFactory.AssignHouseHoldData(householdData);
        residentFactory.AssignGameDatabase(householdData.gamedata);       
        residentFactory.clearResidents();

    }
    [ContextMenu("Genocide")]
    public override void ClearcitizenList()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(GameData);
#endif
        GameData.CitizensMasterList.Clear();
        clearResidents();
    }
}
