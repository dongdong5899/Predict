using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SliderSettingElement : SettingElement<float>
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        _slider.onValueChanged.AddListener(ValueChanged);
    }

    public void ValueChanged(float value)
    {
        enementValue = value;
        OnValueChanged?.Invoke(enementEnum, value);
    }

    public override void SetValue(float value)
    {
        _slider.value = value;
    }
}
