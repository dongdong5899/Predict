using DG.Tweening;
using UnityEngine;

public class WarpEventObject : CubeObject, IEventObject
{
    [SerializeField] private WarpEventObject _targetObject;

    private bool _isEnd = false;

    private Sequence _moveSeq;

    public override void OnAgent()
    {
        base.OnAgent();

        _targetObject.Blink();
    }

    public void PlayEvent(Agent owner)
    {
        float dissolveDuration = 0.5f;
        _isEnd = false;


        owner.OnPlayerSoundEvent?.Invoke(PlayerSoundEvent.Dissolve);
        Material mat = owner.MeshRenderer.material;
        _moveSeq = DOTween.Sequence();
        _moveSeq.Append(mat.DOFloat(1, owner.DissolveHash, dissolveDuration).SetEase(Ease.Linear))
            .AppendCallback(() =>
            {
                owner.OnPlayerSoundEvent?.Invoke(PlayerSoundEvent.Dissolve);
                owner.transform.position = _targetObject.transform.localPosition; 
            })
            .Append(mat.DOFloat(0, owner.DissolveHash, dissolveDuration).SetEase(Ease.Linear))
            .AppendCallback(() => _isEnd = true);
    }

    public bool EventEnd()
    {
        return _isEnd;
    }

    public void Stop()
    {
        _moveSeq.Kill();
        GameManager.Instance.DelaySecCallback(0.2f, () => _isEnd = true);
    }
}
