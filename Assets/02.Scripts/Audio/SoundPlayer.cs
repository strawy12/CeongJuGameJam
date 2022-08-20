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
}
