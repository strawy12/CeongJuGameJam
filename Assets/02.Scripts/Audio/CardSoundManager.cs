using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSoundManager : SoundPlayer
{
    [SerializeField] private AudioClip _cardDrawClip; // 카드 뽑을 때 소리
    [SerializeField] private AudioClip _cardUseClip; // 카드 사용할 떄 소리

    public void DrawCardSound()
    {
        PlayClip(_cardDrawClip);
    }
    public void UseCardSound()
    {
        PlayClip(_cardUseClip);
    }
}
