using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ActionType
{
    Move,
    Rotation,
    Interaction,
    Wait,
}

public class Agent : MonoBehaviour
{
    public readonly int DissolveHash = Shader.PropertyToID("_Dissolve");

    public List<AgentAction> actionList = new List<AgentAction>();
    public AgentAction currentPlayAction;
    public Action<PlayerSoundEvent> OnPlayerSoundEvent;

    public int maxActionCount = 5;

    public bool isActionPlaying = false;

    [HideInInspector] public MeshRenderer MeshRenderer;
    [HideInInspector] public Material Material;

    [SerializeField] private ParticleSystem _playerDieEffect;

    private Vector3 _startPos;

    private void Awake()
    {
        MeshRenderer = transform.Find("Visual").GetComponent<MeshRenderer>();
        Material = MeshRenderer.material;
    }

    private void Start()
    {
        _startPos = transform.position;
    }

    public void Die()
    {
        currentPlayAction?.Stop();

        OnPlayerSoundEvent?.Invoke(PlayerSoundEvent.Die);

        Instantiate(_playerDieEffect, transform.position, Quaternion.identity);
        CameraManager.Instance.Shake(8, 8, 0.1f);
        transform.position = _startPos;
    }

    public void PlayAction()
    {
        if (actionList.Count == 0) return;
        isActionPlaying = true;
        StartCoroutine(PlayActionCoroutine());
    }
    private IEnumerator PlayActionCoroutine()
    {
        currentPlayAction = actionList[0];
        currentPlayAction.Action();
        actionList.RemoveAt(0);
        yield return new WaitUntil(() => currentPlayAction.actionEnd);
        isActionPlaying = false;
        currentPlayAction = null;
    }

    public bool ActionEnqueue(ActionType actionType, bool value = false)
    {
        if (GameManager.Instance.isActionRunning) return false;
        if (IsActionFull())
        {
            //Debug.Log("ÈåÈî...¢¾ ´õ´Â ¾È µé¾î°¡¾Ñ....!!");
            return false;
        }


        Type t = Type.GetType($"Agent{actionType.ToString()}Action");
        AgentAction playerAction = Activator.CreateInstance(t) as AgentAction;
        playerAction.Initialize(this, actionType, value);

        actionList.Add(playerAction);

        UIManager.Instance.AddActionUI(this, actionType, value);

        return true;
    }

    private bool IsActionFull()
        => actionList.Count >= GameManager.Instance.maxActionCount;
}
