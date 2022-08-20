using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : PoolableMono
{
    private Animator _animator;
    private readonly int HASH_COMMAND = Animator.StringToHash("Command");
    public void Init(Vector3 pos)
    {
        transform.position = pos;
        _animator.Play(HASH_COMMAND);
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
