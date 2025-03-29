using DG.Tweening;
using System;
using UnityEngine;

public abstract class CubeObject : MonoBehaviour
{
    private readonly int _blinkHash = Shader.PropertyToID("_Blink");
    private readonly int _alphaHash = Shader.PropertyToID("_Alpha");
    private readonly int _colorHash = Shader.PropertyToID("_Color");
    private readonly int _outLineColorHash = Shader.PropertyToID("_OutLineColor");
    private MeshRenderer _meshRenderer;
    private Material _material;

    public Action<int> OnCubeSoundEvent;

    private float _blinkTime = 1f;

    private bool renderEnable = true;

    public virtual void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
    }

    public virtual void Start() 
    { 

    }

    public virtual void Update()
    {
        float currentAgentYDistance = Mathf.Abs(transform.position.y - GameManager.Instance.CurrentAgent.transform.position.y);
        SetRenderEnable(currentAgentYDistance < 1);
    }

    public virtual void OnAgent()
    {
        Blink();
    }

    public void SetColor(Color color, Color outLineColor)
    {
        _material.SetColor(_colorHash, color);
        _material.SetColor(_outLineColorHash, outLineColor);
    }

    public void SetRenderEnable(bool value)
    {
        if (renderEnable == value) return;

        renderEnable = value;
        _material.SetFloat(_alphaHash, value ? 1f : 0.25f);
    }
    public void Blink()
    {
        _material.DOKill();
        _meshRenderer.material.SetFloat(_blinkHash, 1);
        _material.DOFloat(0, _blinkHash, _blinkTime).SetEase(Ease.OutCirc);
    }
}
