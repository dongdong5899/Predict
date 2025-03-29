using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    [HideInInspector] public Agent CurrentAgent;
    [HideInInspector] public int CurrentAgentIndex;

    [SerializeField] private Transform _levelTrm;

    public int maxActionCount = 0;
    public float actionDelay = 0f;
    public event Action OnPlayAction;
    public bool isActionRunning = false;

    public bool isClear;

    private int _currentActionListPlayCount = 1;
    private int _starState 
        => Mathf.Clamp(3 + _currentStage.actionListPlayCount - _currentActionListPlayCount, 0, 3);
    private Stage _currentStage;

    private void Awake()
    {
        if (LevelManager.Instance == null) return;
        _currentStage = Instantiate(
            LevelManager.Instance.CurrentStageData.transform, _levelTrm)
            .GetComponent<Stage>();
    }

    private void Start()
    {
        if (LevelManager.Instance == null) return;

        InputManager.Instance.EnterDownEvent += HandleEnterDownEvent;
        InputManager.Instance.NumberDownEvent += HandleNumberDownEvent;
        InputManager.Instance.AddActionEvent += HandleAddActionEvent;
        InputManager.Instance.RemoveActionEvent += HandleRemoveActionEvent;

        AgentChange(0);
    }


    public Agent GetAgent(int index)
    {
        return _currentStage.agents[index];
    }

    private void HandleAddActionEvent(ActionType type, bool boolValue)
        => CurrentAgent.ActionEnqueue(type, boolValue);


    private void HandleRemoveActionEvent()
    {
        if (GameManager.Instance.isActionRunning || CurrentAgent.actionList.Count == 0) return;
        CurrentAgent.actionList.RemoveAt(CurrentAgent.actionList.Count - 1);
        UIManager.Instance.RemoveActionUI(CurrentAgent);
    }

    private void HandleNumberDownEvent(int number)
    {
        AgentChange(number - 1);
    }

    private void AgentChange(int index)
    {
        if (_currentStage.agents.Length <= index) return;

        Agent prevAgent = CurrentAgent;
        CurrentAgentIndex = index;
        maxActionCount = _currentStage.agents[CurrentAgentIndex].maxActionCount;
        CurrentAgent = _currentStage.agents[CurrentAgentIndex];

        if (prevAgent == CurrentAgent) return;
        UIManager.Instance.ActionListSelect(prevAgent, CurrentAgent);
        CameraManager.Instance.SetTarget(CurrentAgent.transform);
    }

    private void HandleEnterDownEvent()
    {
        if (isActionRunning) return;

        int count = 0;
        foreach (Agent agent in _currentStage.agents)
        {
            count += agent.actionList.Count;
        }
        if (count == 0) return;

        StartCoroutine(ActionRunningCoroutine());
    }

    private IEnumerator ActionRunningCoroutine()
    {
        isActionRunning = true;
        List<List<AgentAction>> agentsAction = new List<List<AgentAction>>();
        foreach (Agent agent in _currentStage.agents)
        {
            //agent.actionList.Reverse();
            agentsAction.Add(agent.actionList);
        }
        while (true)
        {
            bool flag = false;
            //모든 큐브에서 액션 하나 실행
            foreach (Agent agent in _currentStage.agents)
            {
                if (agent.actionList.Count == 0) continue;
                agent.PlayAction();
                UIManager.Instance.RemoveActionUI(agent, true);
                flag = true;
            }
            //모든 큐브의 액션 리스트가 비었다면 그만
            if (flag == false) break;

            OnPlayAction?.Invoke();
            //액션이 끝날 때까지 기다리기
            yield return new WaitUntil(() 
                => _currentStage.agents.All(x => x.isActionPlaying == false));
        }
        foreach (Agent agent in _currentStage.agents)
        {
            agent.actionList.Clear();
            UIManager.Instance.ClearActionUI(agent);
        }
        isActionRunning = false;


        if (isClear) yield break;

        _currentActionListPlayCount++;
        UIManager.Instance.UpdataActionCountUI(_currentActionListPlayCount);
        UIManager.Instance.UpdataStartCondition(_starState);
    }

    public void GameClear()
    {
        isClear = true;
        UIManager.Instance.ShowClearPanel(_starState);
        LevelManager.Instance.UnlockNextStage();
        LevelManager.Instance.SetStageScore(_starState);
    }

    public void WaitCallback(System.Func<bool> waitFunction, UnityAction callback)
    {
        StartCoroutine(WaitCallbackCoroutine(waitFunction, callback));
    }
    public void DelaySecCallback(float time, UnityAction callback)
    {
        StartCoroutine(DelaySecCallbackCoroutine(time, callback));
    }
    public void DelayFrameCallback(int count, UnityAction callback)
    {
        StartCoroutine(DelayFrameCallbackCoroutine(count, callback));
    }

    private IEnumerator WaitCallbackCoroutine(System.Func<bool> waitFunction, UnityAction callback)
    {
        yield return new WaitUntil(waitFunction);
        callback?.Invoke();
    }
    private IEnumerator DelaySecCallbackCoroutine(float time, UnityAction callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }
    private IEnumerator DelayFrameCallbackCoroutine(int count, UnityAction callback)
    {
        for (int i = 0; i < count; i++)
        {
            yield return null;
        }
        callback?.Invoke();
    }
}
