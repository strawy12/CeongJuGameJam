using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameoverPanel;

    public Timer timer;
    public TMP_Text scoreText;


    private void Start()
    {
        EventManager.StartListening("GameOver", GameOver);
    }

    private void GameOver()
    {
        gameoverPanel.SetActive(true);

        scoreText.text = timer._timerText.text;
    }

    private void OnDestroy()
    {
        EventManager.StopListening("GameOver", GameOver);
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("GameOver", GameOver);
    }
}
