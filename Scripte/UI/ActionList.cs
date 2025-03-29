using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ActionList : MonoBehaviour
{
    [SerializeField] private Image _agentImg;
    [SerializeField] private Sprite[] _agentNumberSprites;
    [SerializeField] private Image _actionListPanelImg;
    [SerializeField] private GameObject _actionSlot;

    private CanvasGroup _canvasGroup;

    private List<ActionSlot> actionSlots = new List<ActionSlot>();


    public void Initialize(Agent agent, int index)
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _agentImg.sprite = _agentNumberSprites[index];


        (_actionListPanelImg.transform as RectTransform).sizeDelta = new Vector2(35 + agent.maxActionCount * 60, 80);

        for (int i = 0; i < agent.maxActionCount; i++)
        {
            ActionSlot slot = Instantiate(_actionSlot, _actionListPanelImg.transform).GetComponent<ActionSlot>();
            slot.Initialize();
            actionSlots.Add(slot);
        }

        ActionListSelect(agent == GameManager.Instance.CurrentAgent);
    }

    public void ActionListSelect(bool value)
    {
        _canvasGroup.alpha = value ? 1f : 0.3f;
    }

    public void AddAction(ActionType actionType, bool boolValue)
    {
        for (int i = 0; i < actionSlots.Count; i++)
        {
            if (actionSlots[i].isSeted == false)
            {
                actionSlots[i].SetAction(actionType, boolValue);
                break;
            }
        }
    }
    public void RemoveAction(bool leftStart = false)
    {
        if (leftStart)
        {
            actionSlots[0].Initialize();
            for (int i = 0; i < actionSlots.Count - 1; i++)
            {
                actionSlots[i].CopyActionSlot(actionSlots[i + 1]);
            }
            actionSlots[actionSlots.Count - 1].Initialize();
        }
        else
        {
            for (int i = actionSlots.Count - 1; i >= 0; i--)
            {
                if (actionSlots[i].isSeted == true)
                {
                    actionSlots[i].Initialize();
                    break;
                }
            }
        }
    }
    public void ClearAction()
    {
        for (int i = actionSlots.Count - 1; i >= 0; i--)
        {
            if (actionSlots[i].isSeted == true)
            {
                actionSlots[i].Initialize();
            }
        }
    }
}
