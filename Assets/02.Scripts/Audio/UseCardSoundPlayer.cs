using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseCardSoundPlayer : SoundPlayer
{
    private void Start()
    {
        VFX();
    }
    public void UseCardSound(AudioClip audio)
    {
        
        PlayClip(audio);
    }
}
