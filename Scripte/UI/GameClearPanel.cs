using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameClearPanel : MonoBehaviour
{
    [SerializeField] private Image[] _stars;
    [SerializeField] private Sprite[] _startSprites;
    [SerializeField] private Button _menuBtn, _reTryBtn, _nextStageBtn;

    private RectTransform _rectTrm;

    private void Start()
    {
        if (LevelManager.Instance == null) return;
        _nextStageBtn.onClick.AddListener(() => LevelManager.Instance.LoadNextStage());
        _reTryBtn.onClick.AddListener(() => LevelManager.Instance.LoadNextStage(0));
        _menuBtn.onClick.AddListener(() => SceneManager.LoadScene(0));

        _rectTrm = transform as RectTransform;
        _rectTrm.anchoredPosition = Vector2.up * (840 * ((float)(Screen.height * 1920) / (Screen.width * 1080)));
    }

    public void Show(int starCount)
    {
        _nextStageBtn.interactable = false;
        _reTryBtn.interactable = false;
        _menuBtn.interactable = false;

        for (int i = 0; i < 3; i++)
        {
            _stars[i].sprite = _startSprites[i < starCount ? 0 : 1];
        }

        RectTransform rectTrm = transform as RectTransform;
        Sequence seq = DOTween.Sequence();
        seq.Append(rectTrm.DOAnchorPosY(0, 0.6f).SetEase(Ease.OutBounce))
            .AppendCallback(() => GameManager.Instance.DelayFrameCallback(1, () =>
            {
                _nextStageBtn.interactable = true;
                _reTryBtn.interactable = true;
                _menuBtn.interactable = true;
            }));
    }
}
