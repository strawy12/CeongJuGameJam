using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillizardSkill : Skill
{
    [ContextMenu("11")]
    public override void UsingSkill()
    {
        Billizard billizard = PoolManager.Inst.Pop(Constant.BLLIZARD_NAME) as Billizard;
        billizard.Init(transform.position);

        base.UsingSkill();
    }



    public override void Reset()
    {

    }
}
