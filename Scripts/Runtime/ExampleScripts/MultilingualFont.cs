using MultilingualPlugin;
using TMPro;
using UnityEngine;

public class MultilingualFont : MultilingualBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private MultilingualObject<TMP_FontAsset> _font;

    private void Reset()
    {
        TryGetComponent(out _label);
    }

    public override void Localization(string languages)
    {
        _label.font = _font.Value;
    }
}
