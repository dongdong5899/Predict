using UnityEngine;

[System.Serializable]
public struct ActionIcon
{
    public ActionType action;
    public Sprite[] actionIcon;
}

[CreateAssetMenu(menuName = "SO/ActionIconsSO")]
public class ActionIconsSO : ScriptableObject
{
    public ActionIcon[] actionIcons;
}
