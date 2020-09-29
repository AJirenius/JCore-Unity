using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JCore
{
    [CustomEditor(typeof(AListSOBase), true)]
    public class AListSOEditor : UnityEditor.Editor
    {
        // TODO display entries by id instead of as a list

        private SerializedProperty entriesProp;
        private SerializedProperty searchFolderProp;

        public override void OnInspectorGUI()
        {
            AListSOBase listSO = target as AListSOBase;

            serializedObject.Update();

            entriesProp = serializedObject.FindProperty("entries");
            searchFolderProp = serializedObject.FindProperty("SearchFolder");

            DrawPropertiesExcluding(serializedObject, new string[] { "entries", "SearchFolder" });

            EditorGUILayout.LabelField("Entries", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(entriesProp, true);

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Automatic population", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(searchFolderProp, true);
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
            if (GUILayout.Button("Go"))
            {
                if (EditorApplication.isPlaying) return;
                CheckEntries(listSO, listSO.SearchFolder);
            }
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();
        }

        private void CheckEntries(AListSOBase listSO, string searchFolder)
        {
            if (!string.IsNullOrEmpty(searchFolder))
            {
                string typeName = listSO.GetEntryTypeName();
                Debug.Log("Looking for assets of type " + typeName + " in " + searchFolder);
                string filter = "t:" + typeName;
                string[] guids = AssetDatabase.FindAssets(filter, new string[] { searchFolder });
                Debug.Log("Found: " + guids.Length);
                if (guids.Length > 0)
                {
                    SetEntriesByGUIDs(listSO, guids);
                    listSO.Validate();
                }
            }
        }

        public void SetEntriesByGUIDs(AListSOBase listSO, string[] guids)
        {
            Debug.Log( "Set " + guids.Length + " entries to " + listSO.GetListName());
            Type listType = listSO.GetEntryType();
            List<UnityEngine.Object> validEntries = new List<UnityEngine.Object>();
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(assetPath, listType);
                if (asset == null)
                {
                    Debug.LogWarning( "Asset " + assetPath + " has wrong type for " + listSO.GetListName());
                    continue;
                }
                validEntries.Add(asset);
            }
            listSO.SetEntries(validEntries.ToArray());
            EditorUtility.SetDirty(listSO);
        }
    }
}