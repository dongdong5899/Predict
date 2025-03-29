using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TitlePanel : MonoBehaviour
{
    private RectTransform _rectTrm;
    public RectTransform rectTrm
    {
        get
        {
            if (_rectTrm == null)
            {
                _rectTrm = transform as RectTransform;
            }
            return _rectTrm;
        }
    }
    public abstract void Init(MenuPanels menuPanels);
    public abstract void OnShow();
    public abstract void OnHide();
}
