using Cinemachine;
using DG.Tweening;
using UnityEngine;


public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera _playerCam;

    private CinemachineBasicMultiChannelPerlin _playerCamMultiChannel;
    private Sequence _shakeSeq;

    private void Awake()
    {
        _playerCamMultiChannel = _playerCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void SetTarget(Transform target)
    {
        _playerCam.Follow = target;
    }

    public void Shake(float amplitude, float frequency, float time, Ease ease = Ease.Linear)
    {
        if (_shakeSeq != null && _shakeSeq.IsActive()) _shakeSeq.Kill();
        _shakeSeq = DOTween.Sequence();

        _shakeSeq.Append(DOTween.To(
            () => amplitude,
            value => _playerCamMultiChannel.m_AmplitudeGain = value,
            0, time).SetEase(ease));
        _shakeSeq.Join(DOTween.To(
            () => frequency,
            value => _playerCamMultiChannel.m_FrequencyGain = value,
            0, time).SetEase(ease));
    }
}
