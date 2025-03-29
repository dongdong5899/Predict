using Doryu.JBSave;
using UnityEngine;

public class SettingSaveData : ISavable<SettingSaveData>
{
    public float bgmVolume;
    public float effectVolume;

    public SettingSaveData()
    {
        ResetData();
    }

    public void ResetData()
    {
        bgmVolume = 0.5f;
        effectVolume = 0.5f;
    }

    public void OnLoadData(SettingSaveData classData)
    {
        bgmVolume = classData.bgmVolume;
        effectVolume = classData.effectVolume;
    }

    public void OnSaveData(string savedFileName)
    {

    }
}
