using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : PoolableMono
{
    private Animator _animator;
    private readonly int HASH_Push = Animator.StringToHash("Push");
    public void Init(Vector3 pos)
    {
        transform.position = pos;
        _animator.Play(HASH_Push);
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
