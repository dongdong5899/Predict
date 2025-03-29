using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private StageListSO _stageListSO;

    private int _currentStageIndex = 0;
    public int CurrentStageIndex
    {
        get => _currentStageIndex;
        private set
        {
            if (value >= _stageListSO.stage.Length) return;

            _currentStageIndex = value;
        }
    }
    public int UnlockedStageIndex => _stageListSO.UnlockedStageIndex;
    public int StageCount => _stageListSO.stage.Length;
    public Stage CurrentStageData => _stageListSO.stage[_currentStageIndex];



    private void Awake()
    {
        if (Instance != this) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }

    public void LoadStage(int stageIndex)
    {
        SceneManager.LoadScene("GameScene");
        CurrentStageIndex = stageIndex;
    }
    public void LoadNextStage(int i = 1)
    {
        SceneManager.LoadScene("GameScene");
        CurrentStageIndex += i;
    }
    public void UnlockNextStage()
    {
        if (CurrentStageIndex < _stageListSO.UnlockedStageIndex) return;

        _stageListSO.UnlockedStageIndex++;
        SetStageScore(-1, _stageListSO.UnlockedStageIndex);
    }

    public int GetStageScroe(int index)
        => _stageListSO.score[index];
    public Stage GetStageData(int index)
        => _stageListSO.stage[index];

    /// <summary>
    /// 스테이지의 점수를 설정합니다.
    /// </summary>
    /// <param name="index">
    /// 인덱스를 기입하지 않으면 현재 스테이지를 설정합니다.
    /// </param>
    public void SetStageScore(int score, int index = -1)
    {
        if (index == -1) _stageListSO.SetScore(CurrentStageIndex, score);
        else _stageListSO.SetScore(index, score);
    }

    public void ResetStageData()
    {
        _stageListSO.ResetData();
    }
}
