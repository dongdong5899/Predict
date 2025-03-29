using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public enum PanelType
{
    Main,
    Stage,
    Option,
    Help
}

public class MenuPanels : MonoBehaviour
{
    [SerializeField] private TitlePanel[] _panels;
    [SerializeField] private float _panelChangeDuration = 0.7f;
    [SerializeField] private Button _quitBtn;
    private TitlePanel _currentPanel;

    public bool _canPanelChange = true;

    private void Awake()
    {
        _currentPanel = _panels[0];
        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i].Init(this);
        }
        _quitBtn.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentPanel == _panels[0]) return;
            ChangePanel(PanelType.Main);
        }
    }

    private void QuitGame()
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i].OnHide();
        }
        Application.Quit();
    }

    public void ChangePanel(PanelType menuPanel)
    {
        if (_canPanelChange == false) return;

        _canPanelChange = false;

        _currentPanel.rectTrm.DOAnchorPosX(-1600, _panelChangeDuration).SetEase(Ease.InBack);

        _currentPanel.OnHide();
        _currentPanel = _panels[(int)menuPanel];
        _currentPanel.OnShow();

        _currentPanel.rectTrm.anchoredPosition = new Vector2(1600, 0);
        _currentPanel.rectTrm.DOAnchorPosX(0, _panelChangeDuration).SetEase(Ease.InBack)
            .OnComplete(() => _canPanelChange = true);
    }
}
