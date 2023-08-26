using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MultilingualPlugin
{
    public static class MultilingualData
    {
        private const string multilingualSettingsKey = "Packages/com.text1258.unitymultilingualplugin/MultilingualSettings.asset";
        private static MultilingualPluginSettings _multilingualSettings = new MultilingualPluginSettings();
        private static string _currentLanguegeInPlayMode;
        private static List<bool> _isActiveLanguagesInPlayMode;

        private static IEnumerable<string> IEnumerableLanguages = new List<string>()
        {
            "Afrikaans",
            "Albanian",
            "Amharic",
            "Arabic",
            "Armenian",
            "Assamese",
            "Aymara",
            "Azerbaijani",
            "Bambara",
            "Basque",
            "Belarusian",
            "Bengali",
            "Bhojpuri",
            "Bosnian",
            "Bulgarian",
            "Catalan",
            "Cebuano",
            "Chinese(Simplified)",
            "Chinese(Traditional)",
            "Corsican",
            "Croatian",
            "Czech",
            "Danish",
            "Dhivehi",
            "Dogri",
            "Dutch",
            "English",
            "Esperanto",
            "Estonian",
            "Ewe",
            "Filipino(Tagalog)",
            "Finnish",
            "French",
            "Frisian",
            "Galician",
            "Georgian",
            "German",
            "Greek",
            "Guarani",
            "Gujarati",
            "HaitianCreole",
            "Hausa",
            "Hawaiian",
            "Hebrew",
            "Hindi",
            "Hmong",
            "Hungarian",
            "Icelandic",
            "Igbo",
            "Ilocano",
            "Indonesian",
            "Irish",
            "Italian",
            "Japanese",
            "Javanese",
            "Kannada",
            "Kazakh",
            "Khmer",
            "Kinyarwanda",
            "Konkani",
            "Korean",
            "Krio",
            "Kurdish",
            "Kurdish(Sorani)",
            "Kyrgyz",
            "Lao",
            "Latin",
            "Latvian",
            "Lingala",
            "Lithuanian",
            "Luganda",
            "Luxembourgish",
            "Macedonian",
            "Maithili",
            "Malagasy",
            "Malay",
            "Malayalam",
            "Maltese",
            "Maori",
            "Marathi",
            "Meiteilon(Manipuri)",
            "Mizo",
            "Mongolian",
            "Myanmar(Burmese)",
            "Nepali",
            "Norwegian",
            "Nyanja(Chichewa)",
            "Odia(Oriya)",
            "Oromo",
            "Pashto",
            "Persian",
            "Polish",
            "Portuguese(Portugal, Brazil)",
            "Punjabi",
            "Quechua",
            "Romanian",
            "Russian",
            "Samoan",
            "Sanskrit",
            "ScotsGaelic",
            "Sepedi",
            "Serbian",
            "Sesotho",
            "Shona",
            "Sindhi",
            "Sinhala(Sinhalese)",
            "Slovak",
            "Slovenian",
            "Somali",
            "Spanish",
            "Sundanese",
            "Swahili",
            "Swedish",
            "Tagalog(Filipino)",
            "Tajik",
            "Tamil",
            "Tatar",
            "Telugu",
            "Thai",
            "Tigrinya",
            "Tsonga",
            "Turkish",
            "Turkmen",
            "Twi(Akan)",
            "Ukrainian",
            "Urdu",
            "Uyghur",
            "Uzbek",
            "Vietnamese",
            "Welsh",
            "Xhosa",
            "Yiddish",
            "Yoruba",
            "Zulu"
        };

        public static MultilingualPluginSettings MultilingualSettings
        {
            get
            {
#if UNITY_EDITOR
                _multilingualSettings = AssetDatabase.LoadAssetAtPath<MultilingualPluginSettings>(multilingualSettingsKey);
                if (_multilingualSettings == null)
                {
                    _multilingualSettings = ScriptableObject.CreateInstance<MultilingualPluginSettings>();
                    AssetDatabase.CreateAsset(_multilingualSettings, multilingualSettingsKey);
                    AssetDatabase.SaveAssets();
                }
#endif
                if (_multilingualSettings == null)
                {
                    _multilingualSettings = Resources.Load<MultilingualPluginSettings>(multilingualSettingsKey);
                }
                return _multilingualSettings;
            }
        }

        public static string CurrentLanguage
        {
            get
            {
                if (Application.isPlaying)
                {
                    if (_currentLanguegeInPlayMode == null)
                    {
                        _currentLanguegeInPlayMode = MultilingualSettings.CurrentLanguage;
                    }
                    return _currentLanguegeInPlayMode;
                }
                return MultilingualSettings.CurrentLanguage;
            }
            set
            {
                if (Application.isPlaying)
                {
                    _currentLanguegeInPlayMode = value;
                }
                else
                {
                    MultilingualSettings.CurrentLanguage = value;
                }
                OnLanguageChange?.Invoke(value);
            }
        }

        public static List<bool> IsActiveLanguages
        {
            get
            {
                if (Application.isPlaying)
                {
                    if (_isActiveLanguagesInPlayMode == null)
                    {
                        _isActiveLanguagesInPlayMode = MultilingualSettings.IsActiveLanguages;
                    }
                    return _isActiveLanguagesInPlayMode;
                }
                return MultilingualSettings.IsActiveLanguages;
            }
        }

        public static int CurrentLanguageIndex
        {
            get
            {
                return Languages.IndexOf(CurrentLanguage);
            }
            set
            {
                CurrentLanguage = Languages[value];
            }
        }

        public static Dictionary<string, bool> IsActiveEveryLanguages => Languages.Zip(IsActiveLanguages, (k, v) => new { k, v })
            .ToDictionary(x => x.k, x => x.v);


        public static List<string> Languages => IEnumerableLanguages.ToList();

        public static Dictionary<string, int> ActiveLanguagesWithIndexes => IsActiveEveryLanguages.Where(x => x.Value == true)
            .ToDictionary(k => k.Key, v => Languages.IndexOf(v.Key));

        public static List<string> ActiveLanguages => ActiveLanguagesWithIndexes.Select(x => x.Key).ToList();

        public static Action<string> OnLanguageChange;
    }
}