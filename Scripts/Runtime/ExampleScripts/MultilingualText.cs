using UnityEngine;
using TMPro;
using MultilingualPlugin;

public class MultilingualText : MultilingualBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private MultilingualObject<string> _text;
    [SerializeField] private MultilingualObject<float> _fontSize;

    private void Reset()
    {
        TryGetComponent(out _label);
    }

    public override void Localization(string languages)
    {
        _label.text = _text.Value;
        _label.fontSize = _fontSize.Value;
    }
}
