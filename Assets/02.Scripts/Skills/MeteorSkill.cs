using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSkill : Skill
{
    public override void Reset()
    {
    }


    public override void UsingSkill()
    {
        Debug.Log("dd3");
        Meteor meteor = PoolManager.Inst.Pop(Constant.METEOR_NAME) as Meteor;
        meteor.Init(transform.position);

        base.UsingSkill();
    }


}
