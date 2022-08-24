using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float currentHp;
    public float maxHp;

    public HpBar hpBar;

    private GameOverSound gameOverSound;

    private void Start()
    {
        gameOverSound = FindObjectOfType<GameOverSound>();
    }


    public void InitHp()
    {
        currentHp = maxHp;

        hpBar.SetMaxHealth(maxHp);
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        hpBar.SetHealth(currentHp);

        if (currentHp <= 0)
        {
            EventManager.TriggerEvent("GameOver");
            gameOverSound.OverSound();
        }
    }
    
    public void GetHeal(float healHp)
    {
        currentHp += healHp;
        if (currentHp > maxHp)
            currentHp = maxHp;

        hpBar.SetHealth(currentHp);
    }
}
