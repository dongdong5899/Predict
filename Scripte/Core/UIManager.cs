using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private ActionIconsSO _actionIconsSO;
    [SerializeField] private GameClearPanel _gameClearPanel;
    [SerializeField] private PauseMenuPanel _pauseMenuPanel;
    [SerializeField] private ActionListsUI _actionListsUI;
    [SerializeField] private StageDataListUI _stageDataListUI;

    public Sprite nullSprite;

    public Dictionary<ActionType, Sprite[]> actionIconDictionary = new Dictionary<ActionType, Sprite[]>();

    private void Start()
    {
        if (LevelManager.Instance == null) return;
        foreach (ActionIcon actionIcon in _actionIconsSO.actionIcons)
        {
            actionIconDictionary.Add(actionIcon.action, actionIcon.actionIcon);
        }

        _stageDataListUI.Initialize(
            LevelManager.Instance.CurrentStageData.actionListPlayCount);

        InputManager.Instance.EscDownEvent += HandleEscDownEvent;
    }

    private void HandleEscDownEvent()
    {
        ActivePausePanel(!_pauseMenuPanel.isActive);
    }

    public void ShowClearPanel(int starCount)
    {
        _gameClearPanel.Show(starCount);
    }
    public void ActivePausePanel(bool active)
    {
        if (active)
        {
            if (GameManager.Instance.isClear) return;
            _pauseMenuPanel.Show();
        }
        else
            _pauseMenuPanel.Hide();
    }

    public void ActionListSelect(Agent prevAgent, Agent newAgent)
    {
        _actionListsUI.ActionListSelect(prevAgent, newAgent);
    }
    public void AddActionUI(Agent agent, ActionType actionType, bool boolValue)
    {
        _actionListsUI.AddAction(agent, actionType, boolValue);
    }
    public void RemoveActionUI(Agent agent, bool leftStart = false)
    {
        _actionListsUI.RemoveAction(agent, leftStart);
    }
    public void ClearActionUI(Agent agent)
    {
        _actionListsUI.ClearAction(agent);
    }

    public void UpdataActionCountUI(int count)
        => _stageDataListUI.UpdataActionListPlayCount(count);
    public void UpdataStartCondition(int starState)
        => _stageDataListUI.UpdataStartCondition(starState);
}
