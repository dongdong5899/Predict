using DG.Tweening;
using UnityEngine;

public class MovingCube : CubeObject
{
    [SerializeField] private Vector3 _firstPos;
    [SerializeField] private Vector3 _secondPos;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private LineRenderer _lineRenderer;

    private float _moveDistance = 0;
    private float _time = 0;

    public bool isMoveing { get; private set; }

    private ButtonEventObject _owner;

    public override void Awake()
    {
        base.Awake();
    }

    public void Initialize(ButtonEventObject owner)
    {
        _owner = owner;

        transform.localPosition = _firstPos;

        _moveDistance = (_firstPos - _secondPos).magnitude;
        _time = _moveDistance / _speed;

        LineInitialize();
    }

    public override void Start()
    {
        base.Start();

        SetColor(_owner.color, Color.black);
        _owner.OnButtonEvent += Move;
    }

    public void LineInitialize()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPositions(new Vector3[] { transform.position, transform.position + (_secondPos - _firstPos) });
        Color color = _owner.color;
        color.a = 0.4f;
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
    }

    public void Move(bool isFirstPos)
    {
        Blink();
        isMoveing = true;
        transform.DOLocalMove(isFirstPos ? _secondPos : _firstPos, _time).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Blink();
                isMoveing = false;
            });
    }
}
