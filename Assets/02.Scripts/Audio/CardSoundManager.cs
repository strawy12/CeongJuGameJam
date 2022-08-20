using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSoundManager : SoundPlayer
{
    [SerializeField] private AudioClip _cardDrawClip; // 카드 뽑을 때 소리
    private void Start()
    {
    }
    public void DrawCardSound()
    {
        PlayClip(_cardDrawClip);
    }
    
}
