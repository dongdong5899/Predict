using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public enum ButtonColor
{
    Red,
    Blue,
    Green,
    Yellow,
}

public class ButtonEventObject : CubeObject, IEventObject
{
    public Action<bool> OnButtonEvent;

    public ButtonColor colorEnum = ButtonColor.Red;

    [ColorUsage(true, true)]
    [HideInInspector] public Color color;

    private bool _isOn;
    private List<MovingCube> _cubes = new List<MovingCube>();

    public override void Awake()
    {
        base.Awake();

        Initialize();

        SetColor(Color.black, color);
    }

    [ContextMenu("Initialize")]
    public void Initialize()
    {
        color = ColorEnumToColor(colorEnum);

        GetComponentsInChildren(_cubes);

        _cubes.ForEach(x => x.Initialize(this));
    }

    public Color ColorEnumToColor(ButtonColor colorEnum)
    {
        Color color = Color.white;
        switch (colorEnum)
        {
            case ButtonColor.Red:
                color = Color.red;
                break;
            case ButtonColor.Blue:
                color = Color.blue;
                break;
            case ButtonColor.Green:
                color = Color.green;
                break;
            case ButtonColor.Yellow:
                color = Color.yellow;
                break;
            default:
                break;
        }
        color *= 2;
        return color;
    }

    public void PlayEvent(Agent owner)
    {
        OnCubeSoundEvent?.Invoke(0);

        _isOn = !_isOn;
        OnButtonEvent?.Invoke(_isOn);
    }


    public bool EventEnd()
    {
        return _cubes.All(x => x.isMoveing == false);
    }

    public void Stop()
    {

    }
}
