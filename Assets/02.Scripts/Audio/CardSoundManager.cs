using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSoundManager : SoundPlayer
{
    [SerializeField] private AudioClip _cardDrawClip; // ī�� ���� �� �Ҹ�
    [SerializeField] private AudioClip _cardUseClip; // ī�� ����� �� �Ҹ�

    public void DrawCardSound()
    {
        PlayClip(_cardDrawClip);
    }
    public void UseCardSound()
    {
        PlayClip(_cardUseClip);
    }
}
