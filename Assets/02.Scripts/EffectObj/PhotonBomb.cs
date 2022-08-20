using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonBomb : PoolableMono
{
    private Animator _animator;
    private readonly int HASH_PHONTON = Animator.StringToHash("PhotonBomb");
    public void Init(Vector3 pos)
    {
        transform.position = pos;
        _animator.Play(HASH_PHONTON);
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
