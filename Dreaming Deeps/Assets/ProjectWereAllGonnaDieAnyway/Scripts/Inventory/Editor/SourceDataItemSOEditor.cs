using UnityEngine;
using UnityEditor;
using Mochineko.SimpleReorderableList;
using Mochineko.SimpleReorderableList.Samples.Editor;
using WereAllGonnaDieAnywayNew.InventorySystem;

[CustomEditor(typeof(SourceDataItemSO))]

public class SourceDataItemSOEditor : Editor
{
	private ReorderableList reorderableList;
	private SerializedProperty ItemType;
	private SerializedProperty ItemIcon;
	private SerializedProperty ItemWeight;
    private SerializedProperty DaysWorthRatio;
    private SerializedProperty ResourcePriority;

	private void OnEnable()
	{
		reorderableList = new ReorderableList(
			serializedObject.FindProperty("StatEffectList")
		);
		ItemType = serializedObject.FindProperty("type");
		ItemIcon = serializedObject.FindProperty("icon");
		ItemWeight = serializedObject.FindProperty("weight");
        DaysWorthRatio = serializedObject.FindProperty("daysWorthRatio");
        ResourcePriority = serializedObject.FindProperty("resourcePriority");
		
	}

	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI();

		serializedObject.Update();

		EditorUtility.SetDirty(target);
		

		EditorGUI.BeginChangeCheck();
		{
			EditorFieldUtility.ReadOnlyComponentField(target as MonoBehaviour, this);

			EditorGUILayout.BeginHorizontal();
			SourceDataItemSO _target = (SourceDataItemSO)target;
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Item Type");
			_target.type = (ITEMTYPE)EditorGUILayout.EnumPopup(_target.type);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Item Icon");
			_target.icon = (Sprite)EditorGUILayout.ObjectField(_target.icon, typeof(Sprite), true);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Item Weight");
			_target.weight = EditorGUILayout.FloatField(_target.weight);
			EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Days Worth Ratio");
			_target.daysWorthRatio = EditorGUILayout.IntField(_target.daysWorthRatio);
			EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Resource Priority");
            _target.resourcePriority = EditorGUILayout.IntField(_target.resourcePriority);
            EditorGUILayout.EndHorizontal();

            if (reorderableList != null)
				reorderableList.Layout();
		}
		if (EditorGUI.EndChangeCheck())
		{
			serializedObject.ApplyModifiedProperties();
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

	}
}
