using UnityEngine;
using Doryu.JBSave;
using System;

[CreateAssetMenu(menuName = "SO/StageListSO")]
public class StageListSO : ScriptableObject
{
    private StageSaveData stageSaveData = new StageSaveData();
    public int UnlockedStageIndex
    {
        get => stageSaveData.unlockedStageIndex;
        set
        {
            if (value >= stage.Length) return;

            stageSaveData.unlockedStageIndex = value;
        }
    }
    public Stage[] stage;
    public int[] score => stageSaveData.score;

    private void OnEnable()
    {
        stageSaveData = new StageSaveData(stage.Length);
        stageSaveData.LoadJson("StageData");
    }

    public void SetScore(int index, int score)
    {
        if (stageSaveData.score[index] > score) return;
        stageSaveData.score[index] = score;

        stageSaveData.SaveJson("StageData");
    }

    [ContextMenu("StageReset")]
    public void ResetData()
    {
        stageSaveData.ResetData();
        stageSaveData.SaveJson("StageData");
    }
}