using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : SoundPlayer
{
    private static BGM instance;
    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}