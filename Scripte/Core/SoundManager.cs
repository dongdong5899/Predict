using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum SoundType
{
    BGM,
    Effect
}

public class SoundManager : MonoSingleton<SoundManager>
{
    private float _bgmVolume = 0.5f;
    public float BGMVolume
    {
        get => _bgmVolume;
        set
        {
            _bgmVolume = Mathf.Clamp(value, 0, 1);
            OnChangeBGMVolume?.Invoke(_bgmVolume);
        }
    }
    private float _effectVolume = 0.5f;
    public float EffectVolume
    {
        get => _effectVolume;
        set
        {
            _effectVolume = Mathf.Clamp(value, 0, 1);
            OnChangeEffectVolume?.Invoke(_effectVolume);
        }
    }
    public Action<float> OnChangeBGMVolume;
    public Action<float> OnChangeEffectVolume;

    private void Awake()
    {
        if (Instance == this) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }
}
