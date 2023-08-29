using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MultilingualPlugin;
using static MultilingualPlugin.MultilingualData;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

public class SelectLanguageDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private MultilingualObject<MultilingualObject<string>> _viewLanguagesNames;
    [SerializeField] private LanguagesOrderType _orderType;
    [SerializeField] private List<string> _viewLanguagesOrder;

    public List<string> ViewLanguagesOrder
    {
        get
        {
            if (OrderType == LanguagesOrderType.Default)
            {
                return ActiveLanguages;
            }
            if (_viewLanguagesOrder == null)
            {
                _viewLanguagesOrder = ActiveLanguages;
            }
            if (_viewLanguagesOrder.Count != ActiveLanguages.Count)
            {
                foreach (string language in ActiveLanguages)
                {
                    if (_viewLanguagesOrder.Contains(language) == false)
                    {
                        _viewLanguagesOrder.Add(language);
                    }
                }
                for (int i = 0; i < _viewLanguagesOrder.Count; i++)
                {
                    if (ActiveLanguages.Contains(_viewLanguagesOrder[i]) == false)
                    {
                        _viewLanguagesOrder.RemoveAt(i);
                    }
                }
            }
            return _viewLanguagesOrder;
        }
    }

    public LanguagesOrderType OrderType => _orderType;

    private void Reset()
    {
        TryGetComponent(out _dropdown);
        foreach (MultilingualObject<string> names in _viewLanguagesNames.Values)
        {
            for (int i = 0; i < Languages.Count; i++)
            {
                names.Values[i] = Languages[i];
            }
        }
    }
    
    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(SelectLanguage);
        OnLanguageChange += OnSelectLanguage;
    }
    
    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(SelectLanguage);
        OnLanguageChange -= OnSelectLanguage;
    }
    
    private void Awake()
    {
        List<TMP_Dropdown.OptionData> languagesOptionData = new List<TMP_Dropdown.OptionData>();
        foreach (string language in ViewLanguagesOrder)
        {
            languagesOptionData.Add(new TMP_Dropdown.OptionData(_viewLanguagesNames.Value.ValuesDictionary[language]));
        }
        _dropdown.AddOptions(languagesOptionData);
        _dropdown.value = ViewLanguagesOrder.IndexOf(CurrentLanguage);
    }
    
    private void SelectLanguage(Int32 languageIndex)
    {
        CurrentLanguage = ViewLanguagesOrder[languageIndex];
        TranslateLanguesNames();
    }
    
    private void OnSelectLanguage(string language)
    {
        _dropdown.value = ViewLanguagesOrder.IndexOf(CurrentLanguage);
    }

    private void TranslateLanguesNames()
    {
        _dropdown.captionText.text = _viewLanguagesNames.ValuesDictionary[CurrentLanguage].Values[Languages.IndexOf(CurrentLanguage)];
        for (int i = 0; i < _dropdown.options.Count; i++)
        {
            _dropdown.options[i].text = _viewLanguagesNames.ValuesDictionary[CurrentLanguage].Values[Languages.IndexOf(ViewLanguagesOrder[i])];
        }
    }
}

public enum LanguagesOrderType
{
    Default,
    Custom,
}

#if UNITY_EDITOR
[CustomEditor(typeof(SelectLanguageDropdown))]
public class MultilingualPluginSettingsEditor : Editor
{
    private ReorderableList reorderableViewLanguagesOrderList;

    private SelectLanguageDropdown selectLanguageDropdown
    {
        get
        {
            return target as SelectLanguageDropdown;
        }
    }

    private void OnEnable()
    {
        reorderableViewLanguagesOrderList = new ReorderableList(selectLanguageDropdown.ViewLanguagesOrder, typeof(SelectLanguageDropdown), true, true, false, false);
        reorderableViewLanguagesOrderList.drawHeaderCallback += DrawHeader;
        reorderableViewLanguagesOrderList.drawElementCallback += DrawElement;
        reorderableViewLanguagesOrderList.onMouseDragCallback += OnReorder;
    }

    private void OnDisable()
    {
        reorderableViewLanguagesOrderList.drawHeaderCallback -= DrawHeader;
        reorderableViewLanguagesOrderList.drawElementCallback -= DrawElement;
        reorderableViewLanguagesOrderList.onMouseDragCallback -= OnReorder;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((SelectLanguageDropdown)target), typeof(SelectLanguageDropdown), false);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_dropdown"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_viewLanguagesNames"));
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_orderType"), new GUIContent("Order Type",
            "(Default, this means the order of the location in the plugin settings)"));
        if (EditorGUI.EndChangeCheck())
        {
            reorderableViewLanguagesOrderList = new ReorderableList(selectLanguageDropdown.ViewLanguagesOrder, typeof(SelectLanguageDropdown), true, true, false, false);
        }
        if (selectLanguageDropdown.OrderType == LanguagesOrderType.Custom)
        {
            reorderableViewLanguagesOrderList.DoLayoutList();
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawHeader(Rect rect)
    {
        GUI.Label(rect, "View Languages Order");
    }

    private void DrawElement(Rect rect, int index, bool active, bool focused)
    {
        try
        {
            EditorGUI.LabelField(rect, selectLanguageDropdown.ViewLanguagesOrder[index]);
        }
        catch { }
    }

    private void OnReorder(ReorderableList list)
    {
        EditorUtility.SetDirty(target);
    }
}
#endif