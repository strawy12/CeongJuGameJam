using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSoundPlayer : PoolableMono
{
    bool isDie = false;
    UseCardSoundPlayer useCardSoundPlayer = null;

    Coroutine coroutine = null;
    public override void Reset()
    {
        if (useCardSoundPlayer == null)
            useCardSoundPlayer = GetComponent<UseCardSoundPlayer>();

        isDie = false;
    }
    IEnumerator Die()
    {
        isDie = true;
        yield return new WaitForSeconds(5f);
        PoolManager.Inst.Push(this);
    }
    public void UseSound(AudioClip clip)
    {
        useCardSoundPlayer.UseCardSound(clip);

        if (!isDie)
        {
            coroutine = StartCoroutine(Die());
        }
    }
}
