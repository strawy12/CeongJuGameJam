using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSkill : Skill
{
    [ContextMenu("11")]
    public override void UsingSkill()
    {
        Vampire vampire = PoolManager.Inst.Pop(Constant.VAMPIRE_NAME) as Vampire;
        vampire.Init(transform.position);

        base.UsingSkill();
        GameManager.Inst.StartBloodEffect();
    }



    public override void Reset()
    {

    }
}
