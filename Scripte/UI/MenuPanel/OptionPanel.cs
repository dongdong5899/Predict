using Doryu.JBSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SettingElementEnum
{
    BGMVolume,
    EffectVolume,
}

public class OptionPanel : TitlePanel
{
    public SettingSaveData settingSaveData = new SettingSaveData();

    [SerializeField] private Button _backBtn, _settingResetBtn;
    [SerializeField] private Transform _settings;
    private List<SettingElement<float>> _settingElements = new List<SettingElement<float>>();

    public override void Init(MenuPanels menuPanels)
    {
        _backBtn.onClick.AddListener(() => menuPanels.ChangePanel(PanelType.Main));
        _settingResetBtn.onClick.AddListener(SettingReset);

        settingSaveData.LoadJson("SettingData");
        Debug.Log(settingSaveData.bgmVolume);

        _settings.GetComponentsInChildren(_settingElements);
        _settingElements.ForEach(x => x.OnValueChanged += OnValueChanged);
    }

    private void OnValueChanged(SettingElementEnum enementEnum, float value)
    {
        switch (enementEnum)
        {
            case SettingElementEnum.BGMVolume:
                SoundManager.Instance.BGMVolume = value;
                break;
            case SettingElementEnum.EffectVolume:
                SoundManager.Instance.EffectVolume = value;
                break;
            default:
                break;
        }
    }

    public void SettingReset()
    {
        settingSaveData.ResetData();
        settingSaveData.SaveJson("SettingData");

        _settingElements[(int)SettingElementEnum.BGMVolume].SetValue(settingSaveData.bgmVolume);
        _settingElements[(int)SettingElementEnum.EffectVolume].SetValue(settingSaveData.effectVolume);
    }

    public override void OnHide()
    {
        settingSaveData.bgmVolume = _settingElements[(int)SettingElementEnum.BGMVolume].enementValue;
        settingSaveData.effectVolume = _settingElements[(int)SettingElementEnum.EffectVolume].enementValue;
        settingSaveData.SaveJson("SettingData");
    }

    public override void OnShow()
    {
        _settingElements[(int)SettingElementEnum.BGMVolume].SetValue(settingSaveData.bgmVolume);
        _settingElements[(int)SettingElementEnum.EffectVolume].SetValue(settingSaveData.effectVolume);
    }
}
