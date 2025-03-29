using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StagePanel : TitlePanel
{
    [SerializeField] private StageSelectUI _stageSelectUI;
    [SerializeField] private Button _backBtn, _prevBtn, _nextBtn;
    [SerializeField] private TextMeshProUGUI _pageText;

    private int _currentPageIndex = 0;
    public int CurrentPageIndex
    {
        get => _currentPageIndex;
        set
        {
            if (value < 0)
                _currentPageIndex = _maxPageIndex;
            else if (value > _maxPageIndex)
                _currentPageIndex = 0;
            else
                _currentPageIndex = value;
        }
    }
    private int _maxPageIndex = 0;


    public override void Init(MenuPanels menuPanels)
    {
        _maxPageIndex = (LevelManager.Instance.StageCount - 1) / 10;
        _prevBtn.onClick.AddListener(() => ChangePage(CurrentPageIndex - 1));
        _nextBtn.onClick.AddListener(() => ChangePage(CurrentPageIndex + 1));
        _backBtn.onClick.AddListener(() => menuPanels.ChangePanel(0));
    }
     
    public void ChangePage(int page)
    {
        CurrentPageIndex = page;
        _stageSelectUI.ReloadPage(CurrentPageIndex);
        UpdatePageText();
    }

    private void UpdatePageText()
    {
        _pageText.SetText($"{CurrentPageIndex + 1}/{_maxPageIndex + 1}");
    }

    public override void OnShow()
    {
        ChangePage(0);
    }

    public override void OnHide()
    {

    }
}
