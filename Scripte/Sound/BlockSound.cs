using UnityEngine;

public class BlockSound : SoundPlayer
{
    [SerializeField] private AudioClip[] sounds;

    private CubeObject _cube;

    protected override void Awake()
    {
        base.Awake();
        _cube = GetComponent<CubeObject>();
        _cube.OnCubeSoundEvent += HandlePlayerSoundEvent;
    }

    private void HandlePlayerSoundEvent(int SoundEventIdx)
    {
        if (SoundEventIdx >= sounds.Length) return;

        PlaySoundClip(sounds[SoundEventIdx]);
    }
}
