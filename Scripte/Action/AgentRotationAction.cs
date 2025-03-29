using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRotationAction : AgentAction
{
    private bool _isRightRotation => boolValue;

    private Sequence _rotationSeq;
    private Vector3 _startRot;

    public override void Stop()
    {
        base.Stop();

        _rotationSeq.Kill();
        _owner.transform.eulerAngles = _startRot;
        GameManager.Instance.DelaySecCallback(0.2f, () => actionEnd = true);
    }

    public override void Action()
    {
        base.Action();

        _owner.OnPlayerSoundEvent?.Invoke(PlayerSoundEvent.Turn);

        float duration = 0.2f;
        _startRot = _owner.transform.eulerAngles;
        Vector3 endRot = _startRot + Vector3.up * (_isRightRotation ? 90 : -90);

        _rotationSeq = DOTween.Sequence();
        _rotationSeq.Append(_owner.transform.DORotate(endRot, duration).SetEase(Ease.OutExpo))
            .AppendCallback(() =>
            {
                _owner.transform.eulerAngles = endRot;
                actionEnd = true;
            });
    }
}
