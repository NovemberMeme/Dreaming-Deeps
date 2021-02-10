using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Mochineko.SimpleReorderableList;
using Mochineko.SimpleReorderableList.Samples.Editor;
using WereAllGonnaDieAnywayNew.InventorySystem;
using WereAllGonnaDieAnywayNew;
[CustomEditor(typeof(Inventory))]

public class ActorInventoryEditor : Editor
{
	private ReorderableList reorderableList;
	private SerializedProperty db;
	

	private void OnEnable()
	{
		reorderableList = new ReorderableList(
			serializedObject.FindProperty("ItemsInBag")
		);
		db = serializedObject.FindProperty("db");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		


		EditorGUI.BeginChangeCheck();
		{

			EditorFieldUtility.ReadOnlyComponentField(target as MonoBehaviour, this);

			EditorGUILayout.BeginHorizontal();
			Inventory _target = (Inventory)target;
			EditorGUILayout.PrefixLabel("Game Data DB");
			_target.db = EditorGUILayout.ObjectField(_target.db, typeof(GameDatabaseSO), true) as GameDatabaseSO;
			EditorGUILayout.EndHorizontal();

			if (reorderableList != null)
				reorderableList.Layout();
		}
		if (EditorGUI.EndChangeCheck())
		{
			serializedObject.ApplyModifiedProperties();
		}

	}
}
