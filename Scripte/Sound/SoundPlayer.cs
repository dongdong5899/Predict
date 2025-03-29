using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private SoundType _soundType;

    protected AudioSource AudioCompo { get; private set; }

    private float _defualtVolume = 1;


    protected virtual void Awake()
    {
        AudioCompo = GetComponent<AudioSource>();
        _defualtVolume = AudioCompo.volume;

        switch (_soundType)
        {
            case SoundType.BGM:
                SoundManager.Instance.OnChangeBGMVolume += HandleVolumeChangedEvent;
                HandleVolumeChangedEvent(SoundManager.Instance.BGMVolume);
                break;
            case SoundType.Effect:
                SoundManager.Instance.OnChangeEffectVolume += HandleVolumeChangedEvent;
                HandleVolumeChangedEvent(SoundManager.Instance.EffectVolume);
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        if (SoundManager.Instance == null) return;
        switch (_soundType)
        {
            case SoundType.BGM:
                SoundManager.Instance.OnChangeBGMVolume -= HandleVolumeChangedEvent;
                break;
            case SoundType.Effect:
                SoundManager.Instance.OnChangeEffectVolume -= HandleVolumeChangedEvent;
                break;
            default:
                break;
        }
    }

    private void HandleVolumeChangedEvent(float volume)
    {
        AudioCompo.volume = _defualtVolume * volume;
    }

    public void PlaySoundClip(AudioClip audioClip)
    {
        AudioCompo.PlayOneShot(audioClip);
    }
}
