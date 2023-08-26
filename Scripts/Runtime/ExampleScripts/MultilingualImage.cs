using UnityEngine;
using UnityEngine.UI;
using MultilingualPlugin;

public class MultilingualImage : MultilingualBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private MultilingualObject<Sprite> _sprite;

    private void Reset()
    {
        TryGetComponent(out _image);
    }

    public override void Localization(string languages)
    {
        _image.sprite = _sprite.Value;
    }
}
