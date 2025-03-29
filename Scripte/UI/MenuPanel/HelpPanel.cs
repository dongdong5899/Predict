using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanel : TitlePanel
{
    [SerializeField] private Transform _pageParent;
    [SerializeField] private Button _backBtn, _prevBtn, _nextBtn, _resetBtn, _realResetBtn, _cancelResetBtn;
    [SerializeField] private TextMeshProUGUI _pageText;
    private RectTransform[] _pages;

    private int _currentPage = 0;
    [SerializeField] private OptionPanel _optionPanel;
    [SerializeField] private GameObject _resetCheckPanel;

    public override void Init(MenuPanels menuPanels)
    {
        _pages = new RectTransform[_pageParent.childCount];
        for (int i = 0; i < _pageParent.childCount; i++)
        {
            _pages[i] = _pageParent.GetChild(i) as RectTransform;
        }

        _backBtn.onClick.AddListener(() => menuPanels.ChangePanel(0));
        _nextBtn.onClick.AddListener(() => ChangePage(_currentPage + 1));
        _prevBtn.onClick.AddListener(() => ChangePage(_currentPage - 1));
        _resetBtn.onClick.AddListener(() => ResetCheckPanelActive(true));
        _realResetBtn.onClick.AddListener(ResetData);
        _cancelResetBtn.onClick.AddListener(() => ResetCheckPanelActive(false));
    }

    public void ResetCheckPanelActive(bool active)
    {
        _resetCheckPanel.SetActive(active);
    }

    public void ResetData()
    {
        LevelManager.Instance.ResetStageData();
        _optionPanel.SettingReset();
        ResetCheckPanelActive(false);
    }

    public override void OnHide()
    {

    }

    public override void OnShow()
    {
        ChangePage(0);
    }

    private void ChangePage(int page)
    {
        _pages[_currentPage].DOAnchorPosX(-1000, 0.1f).SetEase(Ease.Linear);

        if (page < 0)
            _currentPage = _pageParent.childCount - 1;
        else if (page >= _pageParent.childCount)
            _currentPage = 0;
        else 
            _currentPage = page;

        _pages[_currentPage].anchoredPosition = new Vector2(1000, 0);
        _pages[_currentPage].DOAnchorPosX(0, 0.1f).SetEase(Ease.Linear);

        _pageText.SetText($"{_currentPage + 1}/{_pageParent.childCount}");
    }
}
