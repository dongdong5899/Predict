using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWaitAction : AgentAction
{
    public override void Action()
    {
        base.Action();

        GameManager.Instance.DelaySecCallback(0.2f, () => actionEnd = true);
    }
}
