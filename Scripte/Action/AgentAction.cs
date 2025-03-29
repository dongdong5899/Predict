using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class AgentAction
{

    protected Agent _owner;

    public bool boolValue;
    public bool actionEnd;
    public ActionType actionType;

    public virtual void Stop()
    {

    }

    public virtual void Action()
    {
        actionEnd = false;
    }

    public virtual AgentAction Initialize(Agent owner, ActionType actionType, bool value = true)
    {
        _owner = owner;
        boolValue = value;


        return this;
    }
}
