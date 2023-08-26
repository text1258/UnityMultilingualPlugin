using UnityEngine;
using MultilingualPlugin;

public abstract class MultilingualBehaviour : MonoBehaviour
{
    private void OnEnable()
    {
        MultilingualData.OnLanguageChange += Localization;
    }

    private void OnDisable()
    {
       MultilingualData.OnLanguageChange -= Localization;
    }

    private void Awake()
    {
        Localization(MultilingualData.CurrentLanguage);
    }

    public abstract void Localization(string languages);
}
