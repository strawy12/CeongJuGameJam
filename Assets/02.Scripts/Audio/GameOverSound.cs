using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSound : SoundPlayer
{
    [SerializeField] private AudioClip gameOverSound;
    public void OverSound()
    {
        PlayClip(gameOverSound);
    }
}
