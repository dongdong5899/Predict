using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class ShooterBullet : MonoBehaviour
{
    private int _lifeTime = 5;

    private Transform _owner;
    private Sequence _moveSeq;

    public void Initialize(Transform owner, int lifeTime)
    {
        _owner = owner;
        _lifeTime = lifeTime;
        transform.SetPositionAndRotation(owner.position, owner.rotation);
        GameManager.Instance.OnPlayAction += Move;
    }

    public void Move()
    {
        if (_lifeTime == 0)
        {
            Die();
            return;
        }
        _lifeTime--;

        float duration = 0.1f;
        _moveSeq = DOTween.Sequence();
        _moveSeq.Append(transform.DOMove(transform.position + _owner.up, duration).SetEase(Ease.OutExpo))
            .AppendCallback(() =>
            {
                Collider[] hitObject 
                    = Physics.OverlapSphere(transform.position, transform.lossyScale.z / 2, 1 << LayerMask.NameToLayer("Obstacle"));
                if (hitObject.Length > 0)
                {
                    Die();
                }
            });
    }

    private void Update()
    {
        Collider[] hitPlayer 
            = Physics.OverlapSphere(transform.position, transform.lossyScale.z / 2, 1 << LayerMask.NameToLayer("Player"));
        if (hitPlayer.Length > 0)
        {
            Agent agent = hitPlayer[0].transform.GetComponentInParent<Agent>();
            Vector3 dis = agent.transform.position - transform.position + Vector3.up * 0.5f;
            if (dis.magnitude < 0.1f)
            {
                agent.Die();
                Die();
            }
        }
    }

    public void Die()
    {
        _moveSeq.Kill();
        GameManager.Instance.OnPlayAction -= Move;
        Destroy(gameObject);
    }
}
