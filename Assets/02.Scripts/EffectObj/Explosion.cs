using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : PoolableMono
{
    private Animator _animator;
    private readonly int HASH_EXPLOSION = Animator.StringToHash("Explosion");
    public void Init()
    {
        _animator.Play(HASH_EXPLOSION);
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
