using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;

    public float currentHp;
    public float maxHP;
    public float increaseHP;
    public float baseHp;

    public float moveSpeed;
    public float increaseMoveSpeed;
    public float maxMoveSpeed;
    public float baseMoveSpeed;

    public float damage;

    private Rigidbody2D rigid;

    public HpBar hpBar;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        hpBar.SetMaxHealth(currentHp);
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + Vector2.down * (moveSpeed / 4) * Time.fixedDeltaTime);
    }


    private void TakeDamage(float damage)
    {
        currentHp -= damage;

        hpBar.SetHealth(currentHp);
    }


    public void IncreaseValue()
    {
        if (currentHp < maxHP)
            currentHp += increaseHP;

        if (moveSpeed < maxMoveSpeed)
            moveSpeed += increaseMoveSpeed;
    }

    public void InitValue()
    {
        currentHp = baseHp;
        moveSpeed = baseMoveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Destroy")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }

}
