using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
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