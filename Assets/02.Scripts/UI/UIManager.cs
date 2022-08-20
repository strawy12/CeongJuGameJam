using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public TMP_Text startText;
    public float fadeSpeed = 1f;

    public GameObject lobbyCanvas;
    public GameObject inGameCanvas;
    public GameObject cardCanvas;
    public SpriteRenderer shadow;

    public GameObject gameoverPanel;

    public Timer timer;
    public TMP_Text scoreText;

    public Player player;

    private void Start()
    {
        EventManager.StartListening("GameOver", GameOver);
    }

    private void Update()
    {
        if (!startText.gameObject.activeSelf) return;

        Color color = startText.color;
        color.a = Mathf.Sin(Time.time * fadeSpeed) * 0.5f + 0.5f;
        startText.color = color;

    }

    public void ContinueGame()
    {
        gameoverPanel.SetActive(false);
        cardCanvas.SetActive(true);

        GameStart();
    }

    public void Menu()
    {
        lobbyCanvas.SetActive(true);

        cardCanvas.SetActive(false);
        inGameCanvas.SetActive(false);
        gameoverPanel.SetActive(false);
        shadow.color = Color.clear;
    }

    public void GameStart()
    {
        if (lobbyCanvas.activeSelf)
        {
            lobbyCanvas.SetActive(false);
            inGameCanvas.SetActive(true);
            cardCanvas.SetActive(true);

            Color color = Color.white;
            color.a = 0f;
            shadow.color = color;
            shadow.DOFade(0.5f, 0.5f);
        }

        player.InitHp();

        EventManager.TriggerEvent("GameStart");
    }

    private void GameOver()
    {
        gameoverPanel.SetActive(true);
        cardCanvas.SetActive(false);
        shadow.color = Color.clear;

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
