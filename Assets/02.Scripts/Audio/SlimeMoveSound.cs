using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveSound : SoundPlayer
{
    [SerializeField] private AudioClip _slimeMoveClip; // ������ �����̴� �Ҹ�

    private static int playSoundCnt = 0;

    private bool canPlay;
    public void SlimeSound()
    {
        if (playSoundCnt < 10 && canPlay == false)
        {
            playSoundCnt++;
            canPlay = true;
        }

        if (!canPlay)
            return;
       
        PlayClip(_slimeMoveClip);
    }

    private void OnDisable()
    {
        if (canPlay)
        {
            playSoundCnt--;
            canPlay = false;
        }
    }


}
