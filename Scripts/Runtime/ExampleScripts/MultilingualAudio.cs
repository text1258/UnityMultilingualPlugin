using MultilingualPlugin;
using UnityEngine;

public class MultilingualAudio : MultilingualBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private MultilingualObject<AudioClip> _audioClip;

    private void Reset()
    {
        TryGetComponent(out _audioSource);
    }

    public override void Localization(string languages)
    {
        _audioSource.clip = _audioClip.Value;
    }
}
