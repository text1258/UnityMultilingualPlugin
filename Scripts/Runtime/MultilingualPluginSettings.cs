using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MultilingualPlugin
{
    public class MultilingualPluginSettings : ScriptableObject
    {
        public string CurrentLanguage = "Esperanto";
        public List<bool> IsActiveLanguages = new List<bool>()
        {
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false
        };

#if UNITY_EDITOR
        public void Save()
        {
            if (Application.isPlaying == false)
            {
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssetIfDirty(this);
            }
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MultilingualPluginSettings))]
    public class MultilingualPluginSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("You can edit MultilingualPpluginSettings in Edit/Project Settings.../Multilingual Plugin");
        }
    }
#endif
}