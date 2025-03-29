using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : TitlePanel
{
    [SerializeField] private Button _stageBtn, _optionBtn, _helpBtn, _quitBtn;

    public override void Init(MenuPanels menuPanels)
    {
        _stageBtn.onClick.AddListener(() => menuPanels.ChangePanel(PanelType.Stage));
        _optionBtn.onClick.AddListener(() => menuPanels.ChangePanel(PanelType.Option));
        _helpBtn.onClick.AddListener(() => menuPanels.ChangePanel(PanelType.Help));
        _quitBtn.onClick.AddListener(Application.Quit);
    }

    public override void OnHide()
    {

    }

    public override void OnShow()
    {

    }
}
