using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSlot : MonoBehaviour
{
    [SerializeField] private Image slotImage, iconImage;
    public Sprite sprite => iconImage.sprite;
    public bool isSeted;

    public void Initialize()
    {
        isSeted = false;
        slotImage.color = Color.gray;
        iconImage.sprite = UIManager.Instance.nullSprite;
    }

    public void SetAction(ActionType actionType, bool boolValur)
    {
        isSeted = true;
        slotImage.color = Color.white;
        iconImage.sprite = UIManager.Instance.actionIconDictionary[actionType][boolValur ? 0 : 1];
    }
    public void CopyActionSlot(ActionSlot actionSlot)
    {
        isSeted = actionSlot.isSeted;
        slotImage.color = isSeted ? Color.white : Color.gray;
        iconImage.sprite = actionSlot.sprite;
    }
}
