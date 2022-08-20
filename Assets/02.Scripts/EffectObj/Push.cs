using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Push : PoolableMono
{
    [SerializeField] private float _moveDistance = 5f;
    private Animator _animator;
    private readonly int HASH_Push = Animator.StringToHash("Push");
    public void Init()
    {
        Vector3 pos = Utils.MainCam.ViewportToWorldPoint(new Vector3(0.5f, 0f));
        Vector3 dest = Utils.MainCam.ViewportToWorldPoint(new Vector3(0.5f, 1f));
        pos.z = 0f;
        dest.z = 0f;
        transform.position = pos;
        _animator.Play(HASH_Push);

        MainCamera.ShakeCamera(1f, 1, 5);

        transform.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(dest, 2f));
        seq.Join(transform.DOScale(Vector3.one * 5f, 1.5f));
        seq.AppendCallback(Release);
    }

    public void Release()
    {
        PoolManager.Inst.Push(this);
    }

    public override void Reset()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }
}
