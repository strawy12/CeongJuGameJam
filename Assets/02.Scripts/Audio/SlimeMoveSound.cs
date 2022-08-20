using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveSound : SoundPlayer
{
    [SerializeField] private AudioClip _slimeMoveClip; // 슬라임 움직이는 소리
    [SerializeField] private float _repeatTime; // 몇초마다 반복할 것인지

    private void Start()
    {
        VFX();
        StartCoroutine("MoveSound()");
    }
    IEnumerator MoveSound()
    {
        while (true)
        {
            PlayClip(_slimeMoveClip);

            yield return new WaitForSeconds(_repeatTime);
        }
    }

}
