using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject _stageSelectBtnPrefab;
    private int _stageCount = 0;


    public void Initialize(int pageIndex)
    {
        int startStageIndex = pageIndex * 10;
        _stageCount = LevelManager.Instance.StageCount;
        for (int i = startStageIndex; i < _stageCount && i < startStageIndex + 10; i++)
        {
            StageSelectBtnUI stageSelectBtnUI
                = Instantiate(_stageSelectBtnPrefab, transform).GetComponent<StageSelectBtnUI>();
            int score;
            if (i <= LevelManager.Instance.UnlockedStageIndex)
                score = LevelManager.Instance.GetStageScroe(i);
            else score = -2;

            stageSelectBtnUI.Initialize(score, i);
        }
    }

    public void ReloadPage(int pageIndex)
    {
        Clear();
        Initialize(pageIndex);
    }

    private void Clear()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
