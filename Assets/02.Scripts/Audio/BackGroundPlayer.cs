using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundPlayer : SoundPlayer
{
    [SerializeField] private AudioClip _backGroundClip;
    // Start is called before the first frame update
    private void Start()
    {
        EventManager.StartListening("GameStart", () => PlayClip(_backGroundClip));
    }
}
