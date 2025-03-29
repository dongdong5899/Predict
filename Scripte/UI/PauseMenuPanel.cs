using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuPanel : MonoBehaviour
{
    [SerializeField] private Image _pauseImage;
    [SerializeField] private Sprite[] _pauseSprites;
    [SerializeField] private Button _menuBtn, _reTryBtn, _gameBtn;

    private RectTransform _rectTrm;
    private float _startYPos;

    [HideInInspector] public bool isActive;

    private void Start()
    {
        if (LevelManager.Instance == null) return;
        _gameBtn.onClick.AddListener(Hide);
        _reTryBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            LevelManager.Instance.LoadNextStage(0);
        });
        _menuBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        });
        _rectTrm = transform as RectTransform;
        _startYPos = 840 * ((float)(Screen.height * 1920) / (Screen.width * 1080));
        _rectTrm.anchoredPosition = Vector2.up * _startYPos;
    }

    public void Show()
    {
        isActive = true;

        Time.timeScale = 0;

        _gameBtn.interactable = false;
        _reTryBtn.interactable=false;
        _menuBtn.interactable = false;

        _pauseImage.sprite = _pauseSprites[0];

        Sequence seq = DOTween.Sequence();
        seq.Append(_rectTrm.DOAnchorPosY(0, 0.6f).SetEase(Ease.OutBounce))
            .AppendCallback(() =>
            {
                _gameBtn.interactable = true;
                _reTryBtn.interactable = true;
                _menuBtn.interactable = true;
            }).SetUpdate(true);
    }

    public void Hide()
    {

        _gameBtn.interactable = false;
        _menuBtn.interactable = false;

        _pauseImage.sprite = _pauseSprites[1];

        Sequence seq = DOTween.Sequence();
        seq.Append(_rectTrm.DOAnchorPosY(_startYPos, 0.6f).SetEase(Ease.InBack))
            .AppendCallback(() =>
            {
                Time.timeScale = 1;
                isActive = false;
            }).SetUpdate(true);
    }
}
