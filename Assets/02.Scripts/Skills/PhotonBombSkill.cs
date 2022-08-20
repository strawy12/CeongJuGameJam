using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonBombSkill : Skill
{

    [ContextMenu("11")]
    public override void UsingSkill()
    {
        PhotonBomb photonBomb = PoolManager.Inst.Pop(Constant.PHOTON_NAME) as PhotonBomb;
        photonBomb.Init(transform.position);

        base.UsingSkill();
    }



    public override void Reset()
    {

    }
}
