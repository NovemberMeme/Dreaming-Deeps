using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Mochineko.SimpleReorderableList;
using Mochineko.SimpleReorderableList.Samples.Editor;
using  WereAllGonnaDieAnywayNew;

[CustomEditor(typeof(GameDatabaseSO))]
public class GameDataSOEditor : Editor
{
	private ReorderableList ItemTemplateList;
	private ReorderableList CitizenMasterList;
	private ReorderableList CreatureSpritePrefabList;
	private ReorderableList CreatureDefaultStatList;
	private ReorderableList CreatureNamesList;
	private ReorderableList HarvestPointDataList;
	private SerializedProperty CitizenBasePrefab;
	private SerializedProperty ResidentBasePrefab;
	private SerializedProperty HarvestpointPrefab;
	private SerializedProperty GlobalInfoProvider;
	private void OnEnable()
	{
		ItemTemplateList = new ReorderableList(
			serializedObject.FindProperty("ItemTemplates")
		);

		CreatureSpritePrefabList = new ReorderableList(
			serializedObject.FindProperty("CreatureSpritePrefabList")
		);

		CreatureDefaultStatList = new ReorderableList(
			serializedObject.FindProperty("CreatureDefaultStatList")
		);

		CreatureNamesList = new ReorderableList(
			serializedObject.FindProperty("CreatureNamesList")
		);

		CitizenMasterList = new ReorderableList(
			serializedObject.FindProperty("CitizensMasterList")
		);

		HarvestPointDataList = new ReorderableList(
			serializedObject.FindProperty("HarvestPointDataList")
		);

		CitizenBasePrefab = serializedObject.FindProperty("CitizenBasePrefab");
		ResidentBasePrefab = serializedObject.FindProperty("ResidentBasePrefab");
		HarvestpointPrefab = serializedObject.FindProperty("HarvestpointPrefab");
        GlobalInfoProvider = serializedObject.FindProperty("GlobalInfoProvider");

	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUI.BeginChangeCheck();
		{
			EditorFieldUtility.ReadOnlyComponentField(target as MonoBehaviour, this);

			EditorGUILayout.BeginHorizontal();
			GameDatabaseSO _target = (GameDatabaseSO)target;
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Citizen Base Prefab");
			_target.CitizenBasePrefab = EditorGUILayout.ObjectField(_target.CitizenBasePrefab, typeof(GameObject), true) as GameObject;
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Resident Base Prefab");
			_target.ResidentBasePrefab = EditorGUILayout.ObjectField(_target.ResidentBasePrefab, typeof(GameObject), true) as GameObject;
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Harvestpoint Base Prefab");
			_target.HarvestpointPrefab = EditorGUILayout.ObjectField(_target.HarvestpointPrefab, typeof(GameObject), true) as GameObject;
			EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Global Info Provider");
            _target.GlobalInfoProvider = EditorGUILayout.ObjectField(_target.GlobalInfoProvider, typeof(GlobalInfoProvider), true) as GlobalInfoProvider;
            EditorGUILayout.EndHorizontal();


            if (ItemTemplateList != null)
				ItemTemplateList.Layout();
			
			if (CreatureSpritePrefabList != null)
				CreatureSpritePrefabList.Layout(); 
			
			if (CreatureDefaultStatList != null)
				CreatureDefaultStatList.Layout();

			if (CreatureNamesList != null)
				CreatureNamesList.Layout();


			if (CitizenMasterList != null)
				CitizenMasterList.Layout();

			if (HarvestPointDataList != null)
				HarvestPointDataList.Layout();
		}
		if (EditorGUI.EndChangeCheck())
		{
			serializedObject.ApplyModifiedProperties();
		}

	}
}
