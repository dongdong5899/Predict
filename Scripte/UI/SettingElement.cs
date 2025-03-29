using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SettingElement<T> : MonoBehaviour
{
    public SettingElementEnum enementEnum;

    [HideInInspector] public T enementValue;
    public Action<SettingElementEnum, T> OnValueChanged;

    public abstract void SetValue(T value);
}
