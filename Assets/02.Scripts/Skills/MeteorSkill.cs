using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSkill : Skill
{
    public override void Reset()
    {
    }

    private void Start()
    {
        UsingSkill();
    }

    public override void UsingSkill()
    {
        Meteor meteor = PoolManager.Inst.Pop(Constant.METEOR_NAME) as Meteor;
        meteor.Init(transform.position);

        base.UsingSkill();
    }


}
