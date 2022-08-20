using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : SoundPlayer
{
    [SerializeField] private AudioClip _buttonClickSound; // ��ư Ŭ���� �� ���� �Ҹ�

    private void PlayButtonSound()
    {
        PlayClip(_buttonClickSound);
    }
}
