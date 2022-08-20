using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSkill : Skill
{
    [ContextMenu("11")]
    public override void UsingSkill()
    {
        Command command = PoolManager.Inst.Pop(Constant.COMMAND_NAME) as Command;
        command.Init(transform.position);

        base.UsingSkill();
    }



    public override void Reset()
    {

    }
}
