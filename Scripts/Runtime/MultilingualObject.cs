using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MultilingualPlugin
{
    [Serializable]
    public class MultilingualObject<T>
    {
        [SerializeField] private List<T> _values = new List<T>(new T[134]);
        public T Value => Values[MultilingualData.Languages.IndexOf(MultilingualData.CurrentLanguage)];

        public List<T> Values
        {
            get
            {
                while (_values.Count < MultilingualData.Languages.Count)
                {
                    _values.Add((T)new T[1].GetValue(0));
                }
                while (_values.Count > MultilingualData.Languages.Count)
                {
                    _values.RemoveAt(_values.Count - 1);
                }
                return _values;
            }
        }

        public Dictionary<string, T> ValuesDictionary => MultilingualData.Languages.Zip(Values, (k, v) => new { k, v })
            .ToDictionary(x => x.k, x => x.v);
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(MultilingualObject<>), true)]
    public class MultilingualObjectDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueslistProperty = property.FindPropertyRelative("_values");
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;
                valueslistProperty.arraySize = MultilingualData.Languages.Count;
                foreach (KeyValuePair<string, int> languageWithIndex in MultilingualData.ActiveLanguagesWithIndexes)
                {
                    EditorGUILayout.PropertyField(valueslistProperty.GetArrayElementAtIndex(languageWithIndex.Value), new GUIContent(languageWithIndex.Key));
                }
                EditorGUI.indentLevel--;
            }
        }
    }
#endif
}