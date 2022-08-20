using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    private void Awake()
    {
        SetBGM();
        SetVFX();
    }
    [SerializeField] private Toggle _bgmToggle;
    [SerializeField] private Toggle _vfxToggle;
    public bool _useBgm;
    public bool _useVFX;

    public void SetBGM()
    {
        _useBgm = _bgmToggle.isOn;
    }
    public void SetVFX()
    {
        _useVFX = _vfxToggle.isOn;
    }
}
