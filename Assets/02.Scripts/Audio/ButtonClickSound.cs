using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : SoundPlayer
{
    [SerializeField] private AudioClip _buttonClickSound; // ��ư Ŭ���� �� ���� �Ҹ�
    // Start is called before the first frame update
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { PlayClip(_buttonClickSound); });
        VFX();
    }

}
