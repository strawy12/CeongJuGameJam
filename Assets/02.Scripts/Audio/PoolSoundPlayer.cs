using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSoundPlayer : PoolableMono
{
    bool isDie = false;
    UseCardSoundPlayer useCardSoundPlayer = null;
    public override void Reset()
    {
        if(useCardSoundPlayer == null)
        useCardSoundPlayer = GetComponent<UseCardSoundPlayer>();
    }
    private void Update()
    {
        if(isDie == false)
        {
            //StartCoroutine("Die");
            isDie = true;
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);
        isDie=true;
        PoolManager.Inst.Push(this);
    }
    public void UseSound(AudioClip clip)
    {
        useCardSoundPlayer.UseCardSound(clip);
    }
}
