using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEventObject : CubeObject, IEventObject
{
    public void PlayEvent(Agent owner)
    {
        GameManager.Instance.GameClear();
    }
     
    public bool EventEnd()
    {
        return false;
    }

    public void Stop()
    {

    }
}
