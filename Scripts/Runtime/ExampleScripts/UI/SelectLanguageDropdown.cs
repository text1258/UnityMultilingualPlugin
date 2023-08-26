using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MultilingualPlugin;
using static MultilingualPlugin.MultilingualData;

public class SelectLanguageDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private MultilingualObject<MultilingualObject<string>> _viewNames;
    
    private void Reset()
    {
        TryGetComponent(out _dropdown);
        foreach (MultilingualObject<string> names in _viewNames.Values)
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
        foreach (string language in ActiveLanguages)
        {
            languagesOptionData.Add(new TMP_Dropdown.OptionData(_viewNames.Value.ValuesDictionary[language]));
        }
        _dropdown.AddOptions(languagesOptionData);
        _dropdown.value = ActiveLanguages.IndexOf(CurrentLanguage);
    }
    
    private void SelectLanguage(Int32 languageIndex)
    {
        CurrentLanguage = ActiveLanguages[languageIndex];
        TranslateLanguesNames();
    }
    
    private void OnSelectLanguage(string language)
    {
        _dropdown.value = ActiveLanguages.IndexOf(CurrentLanguage);
    }

    private void TranslateLanguesNames()
    {
        _dropdown.captionText.text = _viewNames.ValuesDictionary[CurrentLanguage].Values[Languages.IndexOf(CurrentLanguage)];
        for (int i = 0; i < _dropdown.options.Count; i++)
        {
            _dropdown.options[i].text = _viewNames.ValuesDictionary[CurrentLanguage].Values[Languages.IndexOf(ActiveLanguages[i])];
        }
    }
}
