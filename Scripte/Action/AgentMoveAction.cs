using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AgentMoveAction : AgentAction
{
    private bool _isForward => boolValue;

    private Sequence _moveSeq;

    public override void Stop()
    {
        base.Stop();

        _moveSeq.Kill();
        GameManager.Instance.DelaySecCallback(0.2f, () => actionEnd = true);
    }

    public override void Action()
    {
        base.Action();

        _owner.OnPlayerSoundEvent?.Invoke(PlayerSoundEvent.Move);

        float duration = 0.2f;
        Vector3 startPos = _owner.transform.position;
        Vector3 endPos = startPos + _owner.transform.forward * (_isForward ?  1 : -1);

        IEventObject eventObject = null;
        Collider[] forwardGround = Physics.OverlapSphere(endPos + Vector3.down * 0.5f, 0.1f, 1 << LayerMask.NameToLayer("Obstacle"));
        Collider[] forwardObject = Physics.OverlapSphere(endPos + Vector3.up * 0.5f, 0.1f, 1 << LayerMask.NameToLayer("Obstacle"));

        if (forwardObject.Length > 0)
        {
            GameManager.Instance.DelaySecCallback(duration, () => actionEnd = true);
            return;
        }

        if (forwardGround.Length > 0 && forwardGround[0].transform.TryGetComponent(out CubeObject cubeObject))
        {
            cubeObject.OnAgent();
            eventObject = cubeObject as IEventObject;
        }

        _moveSeq = DOTween.Sequence();
        _moveSeq.Append(_owner.transform.DOMove(endPos, duration).SetEase(Ease.OutExpo))
            .AppendCallback(() =>
            {
                _owner.transform.position = endPos;

                if (eventObject != null)
                {
                    eventObject.PlayEvent(_owner);
                    GameManager.Instance.WaitCallback(eventObject.EventEnd, () => actionEnd = true);
                }
                else if (forwardGround.Length == 0)
                {
                    _owner.Die();
                }
                else
                {
                    actionEnd = true;
                }
            });
    }
}
