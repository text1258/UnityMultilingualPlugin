using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using static MultilingualPlugin.MultilingualData;

namespace MultilingualPlugin
{
#if UNITY_EDITOR
    public class MultilingualPluginSettingsProvider : SettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new MultilingualPluginSettingsProvider("Project/Multilingual Plugin", SettingsScope.Project);
        }

        public MultilingualPluginSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords) { }

        public override void OnGUI(string searchContext)
        {
            if (Application.isPlaying)
            {
                GUI.enabled = false;
            }
            EditorGUI.BeginChangeCheck();
            CurrentLanguageIndex = EditorGUILayout.Popup("CurrentLanguage", CurrentLanguageIndex, Languages.ToArray());
            EditorGUILayout.LabelField("What languages do you want to use in the plugin");
            EditorGUI.indentLevel++;
            for (int i = 0; i < Languages.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Languages[i], GUILayout.Width(175));
                IsActiveLanguages[i] = EditorGUILayout.Toggle(IsActiveLanguages[i]);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;
            if (EditorGUI.EndChangeCheck())
            {
                if (Application.isPlaying)
                {
                    return;
                }
                MultilingualSettings.Save();
            }
        }
    }
#endif
}