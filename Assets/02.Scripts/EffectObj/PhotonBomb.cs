using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonBomb : PoolableMono
{
    private Animator _animator;
    public void Init(Vector3 pos)
    {
        transform.position = pos;
        _animator.Play("PothonBomb");
    }

    public void ShakeCamera()
    {
        MainCamera.ShakeCamera(0.3f);
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
