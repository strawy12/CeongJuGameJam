using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSoundManager : SoundPlayer
{
    [SerializeField] private AudioClip _cardDrawClip; // ī�� ���� �� �Ҹ�
    private void Start()
    {
    }
    public void DrawCardSound()
    {
        PlayClip(_cardDrawClip);
    }
    
}
