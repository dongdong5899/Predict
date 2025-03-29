using Doryu.JBSave;

public class StageSaveData : ISavable<StageSaveData>
{
    public int unlockedStageIndex = 0;
    public int scoreCount;
    public int[] score;

    public StageSaveData()
    {
        score = new int[10];
        ResetData();
    }
    public StageSaveData(int stageCount)
    {
        scoreCount = stageCount;
        score = new int[scoreCount];
        ResetData();
    }

    public void ResetData()
    {
        unlockedStageIndex = 0;
        for (int i = 0; i < score.Length; i++)
        {
            score[i] = -1;
        }
    }
    public void OnLoadData(StageSaveData classData)
    {
        unlockedStageIndex = classData.unlockedStageIndex;
        for (int i = 0; i < score.Length && i < classData.score.Length; i++)
        {
            score[i] = classData.score[i];
        }
    }

    public void OnSaveData(string savedFileName)
    {

    }
}
