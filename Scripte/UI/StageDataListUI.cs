using TMPro;
using UnityEngine;

public class StageDataListUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stageLevel;
    [SerializeField] private TextMeshProUGUI _actionListPlayCount;
    [SerializeField] private TextMeshProUGUI[] _startCondition;

    private string[] _starStr = { "��", "�ڡ�", "�ڡڡ�" };

    public void Initialize(int maxActionPlayCount)
    {
        int stageLevel = LevelManager.Instance.CurrentStageIndex + 1;
        _stageLevel.SetText($"< Stage {stageLevel} >");
        for (int i = 0; i < 3; i++)
        {
            _startCondition[i].SetText(
                $"����� {maxActionPlayCount - i + 2}ȸ ���Ϸ� ���� - {_starStr[i]}");
        }
        UpdataActionListPlayCount(0);
        UpdataStartCondition(3);
    }

    public void UpdataStartCondition(int starState)
    {
        for (int i = 0; i < _startCondition.Length; i++)
        {
            _startCondition[i].color =
                i < starState ? Color.white : Color.gray;
        }
    }
    public void UpdataActionListPlayCount(int count)
    {
        _actionListPlayCount.SetText($"��� ���� Ƚ��: {count}");
    }
}
