using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundPlayer : SoundPlayer
{
    [SerializeField] private AudioClip _backGroundClip;
    // Start is called before the first frame update
    void Start()
    {
        PlayClip(_backGroundClip);
    }
}
