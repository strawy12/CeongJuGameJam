using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    private static GameOverUI instance;
    public static GameOverUI Instance => instance;

    public GameObject gameoverPanel;

    public Timer timer;
    public TMP_Text scoreText;


    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void GameOver()
    {
        gameoverPanel.SetActive(true);

        timer.StopTimer();
        scoreText.text = timer._timerText.text;
    }
}
