using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : PoolableMono
{
    private Animator _animator;
    public void Init()
    {
        _animator.Play("Explosion");
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
