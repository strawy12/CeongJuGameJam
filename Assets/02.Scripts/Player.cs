using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float currentHp;
    public float maxHp;

    public HpBar hpBar;

    private void Start()
    {
        EventManager.StartListening("GameStart", InitHp);

        hpBar.SetMaxHealth(maxHp);
    }

    private void InitHp()
    {
        currentHp = maxHp;

        hpBar.SetHealth(currentHp);
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        hpBar.SetHealth(currentHp);

        if (currentHp <= 0)
        {
            EventManager.TriggerEvent("GameOver");
        }
    }
}
