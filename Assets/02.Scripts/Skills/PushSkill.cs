using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSkill : Skill
{
    [ContextMenu("11")]
    public override void UsingSkill()
    {
        Push push = PoolManager.Inst.Pop(Constant.PUSH_NAME) as Push;
        push.Init(transform.position);

        base.UsingSkill();
    }



    public override void Reset()
    {

    }
}
