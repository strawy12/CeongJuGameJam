using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _maxTImer;
    public TMP_Text _timerText;
    private float _nowTime;

    private void Awake()
    {
        _timerText = GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        _nowTime = _maxTImer;
        StartCoroutine(TimerCoroutine());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    private IEnumerator TimerCoroutine()
    {
        while(true)
        {
            if (_nowTime / 60 > 99) yield break;

            _timerText.text =string.Format( "{0:00} : {1:00}",(int)_nowTime / 60, (int)_nowTime % 60);
            _nowTime += 1f;
            yield return new WaitForSeconds(1f);
        }


    }

}
