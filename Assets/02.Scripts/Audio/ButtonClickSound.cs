using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : SoundPlayer
{
    [SerializeField] private AudioClip _buttonClickSound; // 버튼 클릭할 때 나는 소리
    // Start is called before the first frame update
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { PlayClip(_buttonClickSound); });
        VFX();
    }

}
