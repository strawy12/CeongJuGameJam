using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : SoundPlayer
{
    [SerializeField] private AudioClip _buttonClickSound; // 버튼 클릭할 때 나는 소리

    private void PlayButtonSound()
    {
        PlayClip(_buttonClickSound);
    }
}
