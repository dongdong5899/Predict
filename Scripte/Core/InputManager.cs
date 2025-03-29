using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    public event Action<ActionType, bool> AddActionEvent;
    public event Action<int> NumberDownEvent;
    public event Action EnterDownEvent;
    public event Action RemoveActionEvent;

    public event Action EscDownEvent;

    [SerializeField] private KeyCode _interactionKey;


    private KeyCode[] numberKeys =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3
    };

    private void Update()
    {
        //ActionInput
        MoveCheck();
        RotationCheck();
        InteractionCheck();
        WaitCheck();

        //ActionControl
        EnterCheck();
        BackspaceCheck();

        //System
        NumberKeyCheck();
        EscDownCheck();
    }

    #region ActionCheck
    private void MoveCheck()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddActionEvent?.Invoke(ActionType.Move, true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            AddActionEvent?.Invoke(ActionType.Move, false);
        }
    }

    private void RotationCheck()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            AddActionEvent?.Invoke(ActionType.Rotation, true);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            AddActionEvent?.Invoke(ActionType.Rotation, false);
        }
    }

    private void InteractionCheck()
    {
        if (Input.GetKeyDown(_interactionKey))
        {
            AddActionEvent?.Invoke(ActionType.Interaction, true);
        }
    }

    private void WaitCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            AddActionEvent?.Invoke(ActionType.Wait, true);
        }
    }
    #endregion


    private void EscDownCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscDownEvent?.Invoke();
        }
    }

    private void NumberKeyCheck()
    {
        for (int i = 0; i < numberKeys.Length; i++)
        {
            if (Input.GetKeyDown(numberKeys[i]))
            {
                NumberDownEvent?.Invoke(i + 1);
            }
        }
    }

    private void BackspaceCheck()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            RemoveActionEvent?.Invoke();
        }
    }
    private void EnterCheck()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            EnterDownEvent?.Invoke();
        }
    }
}
