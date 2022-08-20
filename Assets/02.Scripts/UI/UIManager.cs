using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject lobbyCanvas;
    public GameObject inGameCanvas;
    public GameObject cardCanvas;

    public GameObject gameoverPanel;

    public Timer timer;
    public TMP_Text scoreText;

    private void Start()
    {
        EventManager.StartListening("GameOver", GameOver);
    }

    public void ContinueGame()
    {
        gameoverPanel.SetActive(false);

        GameStart();
    }

    public void Menu()
    {
        lobbyCanvas.SetActive(true);

        cardCanvas.SetActive(false);
        inGameCanvas.SetActive(false);
        gameoverPanel.SetActive(false);
    }

    public void GameStart()
    {
        if (lobbyCanvas.activeSelf)
        {
            lobbyCanvas.SetActive(false);
            inGameCanvas.SetActive(true);
            cardCanvas.SetActive(true);
        }

        EventManager.TriggerEvent("GameStart");
    }

    private void GameOver()
    {
        gameoverPanel.SetActive(true);
        cardCanvas.SetActive(false);

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
