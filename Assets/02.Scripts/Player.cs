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
<<<<<<< HEAD
        
        currentHp = maxHp;
=======
        EventManager.StartListening("GameStart", InitHp);
>>>>>>> 14abbdfdf941bf6f1b83f4b56e317630e48741cb

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
    
    public void GetHeal(float healHp)
    {
        currentHp += healHp;
        if (currentHp > maxHp)
            currentHp = maxHp;

        hpBar.SetHealth(currentHp);
    }
}
