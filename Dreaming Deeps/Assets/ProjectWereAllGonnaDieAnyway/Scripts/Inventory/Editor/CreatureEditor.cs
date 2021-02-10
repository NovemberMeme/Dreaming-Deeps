using UnityEngine;
using UnityEditor;
using Mochineko.SimpleReorderableList;
using Mochineko.SimpleReorderableList.Samples.Editor;
using WereAllGonnaDieAnywayNew;

[CustomEditor(typeof(Creature))]

public class CreatureEditor : Editor
{
	private SerializedProperty actorType;
	private SerializedProperty id;
  

	private void OnEnable()
	{
		
		actorType = serializedObject.FindProperty("Type");
		id = serializedObject.FindProperty("CreatureID");

	}

	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI();

		serializedObject.Update();

		EditorGUI.BeginChangeCheck();
		{			

            ////EditorFieldUtility.ReadOnlyComponentField(target as MonoBehaviour, this);


            Creature _target = (Creature)target;
            base.OnInspectorGUI();
            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.PrefixLabel("Character Name");
            //         _target.CharacterName = EditorGUILayout.TextField(_target.CharacterName);
            //EditorGUILayout.EndHorizontal();



            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Type");
            _target.Type = (CREATURE_TYPE)EditorGUILayout.EnumPopup(_target.Type);

            EditorGUILayout.EndHorizontal();

            if (_target.Type == CREATURE_TYPE.NPC)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("ID");
                _target.CreatureID = EditorGUILayout.IntField(_target.CreatureID);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

          


            //if (StatList != null)
            //	StatList.Layout();

            //EditorGUILayout.Space();

            ////EditorGUILayout.BeginHorizontal();
            ////EditorGUILayout.PrefixLabel("Currency");
            ////_target.currency = EditorGUILayout.FloatField(_target.currency);
            ////EditorGUILayout.EndHorizontal();

        }
        if (EditorGUI.EndChangeCheck())
		{
			serializedObject.ApplyModifiedProperties();
		}

	}
}
