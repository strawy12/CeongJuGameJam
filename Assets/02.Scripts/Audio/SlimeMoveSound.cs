using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveSound : SoundPlayer
{
    [SerializeField] private AudioClip _slimeMoveClip; // ������ �����̴� �Ҹ�
    [SerializeField] private float _repeatTime; // ���ʸ��� �ݺ��� ������

    private void Start()
    {
        StartCoroutine("MoveSound()");
    }
    IEnumerator MoveSound()
    {
        while (true)
        {
            PlayClip(_slimeMoveClip);

            yield return new WaitForSeconds(_repeatTime);
        }
    }

}
