#if UNITY_EDITOR

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


namespace Dands.Pool
{
    [CustomEditor(typeof(DPool))]
    public class DPoolEditor : Editor
    {
        private ReorderableList list;

        private void OnEnable()
        {
            list = new ReorderableList(serializedObject, serializedObject.FindProperty("itemsPool"),
                true, true, true, true);

            list.drawElementCallback = (Rect rect, int index, bool selected, bool focused) =>
            {
                SerializedProperty itemData = list.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                rect.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(rect, itemData, GUIContent.none);
            };
            list.drawHeaderCallback = (Rect rect) => { GUI.Label(rect, "Items"); };
            list.onRemoveCallback = (ReorderableList list) =>
            {
                if (EditorUtility.DisplayDialog("Delete", "Do you really want to delete this item?", "Ok", "Cancel"))
                {
                    ReorderableList.defaultBehaviours.DoRemoveButton(list);
                }
            };

            list.onAddCallback = (ReorderableList list) =>
            {
                if (list.serializedProperty != null)
                {
                    list.serializedProperty.arraySize++;
                    list.index = list.serializedProperty.arraySize - 1;
                    SerializedProperty itemData = list.serializedProperty.GetArrayElementAtIndex(list.index);
                }
                else
                {
                    ReorderableList.defaultBehaviours.DoAddButton(list);
                }
            };
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space();
            serializedObject.Update();
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif