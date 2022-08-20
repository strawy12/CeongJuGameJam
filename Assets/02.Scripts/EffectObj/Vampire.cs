using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Vampire : PoolableMono
{
    private Animator _animator;

    public override void Reset()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    [ContextMenu("11")]
    public void Init(Vector3 pos)
    {
        _animator.Play("New Animation");
        transform.localScale = Vector3.zero;
        transform.position = pos;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(Vector3.one, 1f));
        seq.Append(transform.DOScale(Vector3.zero, 1f).SetDelay(3f));
        seq.AppendCallback(Release);
    }

    public void Release()
    {
        PoolManager.Inst.Push(this);
    }
}
