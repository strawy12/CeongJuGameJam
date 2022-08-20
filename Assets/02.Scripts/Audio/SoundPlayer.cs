using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    private AudioSource _audioSource = null;
    private AudioSetting _audioSetting = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSetting = FindObjectOfType<AudioSetting>();
    }
    protected void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();

    }
    protected void VFX()
    {
        if (_audioSetting._useVFX)
        {
            _audioSource.volume = 1.0f;
        }
        else
        {
            _audioSource.volume = 0;
        }
    }
    protected void BGM()
    {
        if (_audioSetting._useBgm)
        {
            _audioSource.volume = 1.0f;
        }
        else
        {
            _audioSource.volume = 0;
        }
    }

}
