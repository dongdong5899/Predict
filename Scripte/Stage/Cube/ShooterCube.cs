using TMPro;
using UnityEngine;

public class ShooterCube : CubeObject
{
    private readonly int _outLineHash = Shader.PropertyToID("_OutLine");

    [SerializeField] private ShooterBullet _bullet;
    [SerializeField] private TextMeshPro _coolTimeText;
    [SerializeField] private int _fireCooltime = 5;
    [SerializeField] private int _currentFireCooltime = 1;
    [SerializeField] private int _bulletLifetime = 5;

    public override void Start()
    {
        base.Start();

        GameManager.Instance.OnPlayAction += FireCoolDown;
    }

    private void FireCoolDown()
    {
        _currentFireCooltime--;

        if (_currentFireCooltime == 0)
        {
            Fire();
            Blink();
        }

        _coolTimeText.SetText($"{_currentFireCooltime}");
    }

    private void Fire()
    {
        OnCubeSoundEvent?.Invoke(0);

        _currentFireCooltime = _fireCooltime;
        ShooterBullet bullet = Instantiate(_bullet, transform).GetComponent<ShooterBullet>();
        bullet.Initialize(transform, _bulletLifetime);
        bullet.Move();
    }
}
