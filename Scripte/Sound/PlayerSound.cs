using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSoundEvent
{
    Move,
    Turn,
    Dissolve,
    CubeMove,
    Die
}

public class PlayerSound : SoundPlayer
{
    [SerializeField] private AudioClip _moveSound;
    [SerializeField] private AudioClip _turnSound;
    [SerializeField] private AudioClip _dissolveSound;
    [SerializeField] private AudioClip _cubeMoveSound;
    [SerializeField] private AudioClip _dieSound;

    private Agent _agent;

    protected override void Awake()
    {
        base.Awake();
        _agent = GetComponent<Agent>();
        _agent.OnPlayerSoundEvent += HandlePlayerSoundEvent;
    }

    private void HandlePlayerSoundEvent(PlayerSoundEvent SoundEvent)
    {
        switch (SoundEvent)
        {
            case PlayerSoundEvent.Move:
                PlaySoundClip(_moveSound);
                break;
            case PlayerSoundEvent.Turn:
                PlaySoundClip(_turnSound);
                break;
            case PlayerSoundEvent.Dissolve:
                PlaySoundClip(_dissolveSound);
                break;
            case PlayerSoundEvent.CubeMove:
                PlaySoundClip(_cubeMoveSound);
                break;
            case PlayerSoundEvent.Die:
                PlaySoundClip(_dieSound);
                break;
            default:
                break;
        }
    }
}
