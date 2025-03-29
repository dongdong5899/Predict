using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionListsUI : MonoBehaviour
{
    [SerializeField] private GameObject _actionList;
    private Dictionary<Agent, ActionList> agentToActionListDictionary 
        = new Dictionary<Agent, ActionList>();

    private void Start()
    {
        if (LevelManager.Instance == null) return;

        int agentCount = LevelManager.Instance.CurrentStageData.agents.Length;

        for (int i = agentCount - 1; i >= 0; i--)
        {
            Agent agent = GameManager.Instance.GetAgent(i);
            ActionList actionList = Instantiate(_actionList, transform).GetComponent<ActionList>();
            actionList.Initialize(agent, i);
            agentToActionListDictionary.Add(agent, actionList);

            actionList.ActionListSelect(agent == GameManager.Instance.CurrentAgent);
        }
    }

    public void ActionListSelect(Agent prevAgent, Agent newAgent)
    {
        if (agentToActionListDictionary.Count == 0) return;
        agentToActionListDictionary[prevAgent].ActionListSelect(false);
        agentToActionListDictionary[newAgent].ActionListSelect(true);
    }
    public void AddAction(Agent agent, ActionType actionType, bool boolValue)
        => agentToActionListDictionary[agent].AddAction(actionType, boolValue);
    public void RemoveAction(Agent agent, bool leftStart)
        => agentToActionListDictionary[agent].RemoveAction(leftStart);
    public void ClearAction(Agent agent)
        => agentToActionListDictionary[agent].ClearAction();
}
