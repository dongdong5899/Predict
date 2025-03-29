using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectBtnUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Image[] _starImages; 
    [SerializeField] private Sprite _lockSprites;
    [SerializeField] private Sprite[] _starSprites;
    [SerializeField] private Button _stageSelectBtn;

    public bool isLocked {  get; private set; }
    public bool isCleared {  get; private set; }

    

    public void Initialize(int score, int index)
    {
        _textMeshProUGUI.text = $"{index + 1}";
        _stageSelectBtn.onClick.AddListener(() => LevelManager.Instance.LoadStage(index));

        isLocked = score == -2;
        isCleared = score >= 0;
        _stageSelectBtn.interactable = !isLocked;

        _lockImage.color = isLocked ? Color.white : new Color(1, 1, 1, 0);

        if (isCleared)
        {
            for (int i = 0; i < _starImages.Length; i++)
            {
                _starImages[i].color = Color.white;
                _starImages[i].sprite = _starSprites[i < score ? 1 : 0];
            }
        }
        else
        {
            for (int i = 0; i < _starImages.Length; i++)
            {
                _starImages[i].color = new Color(1, 1, 1, 0);
            }
        }
    }
}
