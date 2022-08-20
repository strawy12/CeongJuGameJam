using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] private Toggle _bgmToggle;
    [SerializeField] private Toggle _vfxToggle;
    [SerializeField] private AudioMixer _mixer;
    private void Awake()
    {
        SetBGM();
        SetVFX();
    }

    public void SetBGM()
    {
        _mixer.SetFloat("BGM",  _bgmToggle.isOn ? -80f:-20f);
    }
    public void SetVFX()
    {
        _mixer.SetFloat("Effect", _vfxToggle.isOn ? -80f : 20f);
    }
}
