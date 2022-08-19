using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Meteor : PoolableMono
{
    [SerializeField] private float _distance;
    
    /// <summary>
    /// Reverse Speed
    /// </summary>
    [SerializeField] private float _moveDuration;
    
    public void Init(Vector3 dest)
    {
        transform.localScale = Vector3.zero;
        transform.position = SetPos(dest);
        ChangeFace(dest, transform.position);

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(dest, _moveDuration).SetEase(Ease.Linear));
        seq.Join(transform.DOScale(Vector3.one * 2f, _moveDuration / 2f).SetEase(Ease.Linear));
        seq.AppendCallback(Release);
    }

    private Vector3 SetPos(Vector3 dest)
    {
        float radAngle = Random.Range(90f, 180f) * Mathf.Deg2Rad;

        Vector3 pos = new Vector3(Mathf.Cos(radAngle), Mathf.Sin(radAngle)) * _distance;
        pos -= dest;

        return pos;
    }
    private void ChangeFace(Vector3 dest, Vector3 pos)
    {
        Vector3 dir = dest - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Release()
    {
        Explosion explosion = PoolManager.Inst.Pop(Constant.EXPLOSION_NAME) as Explosion;
        explosion.transform.position = transform.position;
        explosion.Init();
        transform.DOScale(Vector3.zero, _moveDuration / 2f).SetEase(Ease.OutBounce).OnComplete(() => PoolManager.Inst.Push(this));
        
    }


    public override void Reset()
    {
    }
}
