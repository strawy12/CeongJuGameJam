using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IKnockback, IHittable
{
    private Rigidbody2D _rigid = null;
    private bool _isKnockbacking;

    public Vector3 HitPoint { get; set; }

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

    private bool isSlowing;
    private bool isStunning;

    public HpBar hpBar;

    private void Awake()
    {
        if (hpBar == null)
        {
            hpBar = GetComponentInChildren<HpBar>();
        }
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        hpBar.SetMaxHealth(currentHp);
    }

    private void FixedUpdate()
    {
        if (isStunning)
            return;
        _rigid.MovePosition(_rigid.position + Vector2.down * (moveSpeed / 4) * Time.fixedDeltaTime);
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

    public void GetHit(float damage, GameObject damageDealer, float duration)
    {
        StartCoroutine(HitDamageCoroutine(damage, duration));
    }

    public void GetCrowdCtrl(ECrowdControlType type, float amount, float duration)
    {
        switch (type)
        {
            case ECrowdControlType.Slow:
                // 스피드 줄이기
                if (isSlowing)
                    return;

                StartCoroutine(SlowCoroutine(amount, duration));
                break;
            case ECrowdControlType.Stun:

                // 움직임 멈춤
                StartCoroutine(StunCoroutine(duration));
                break;
            case ECrowdControlType.Heal:
                // 데미지만 닳게 하기

                StartCoroutine(HitDamageCoroutine(amount, duration));
                break;
        }
    }

    private IEnumerator SlowCoroutine(float amount, float duration)
    {
        moveSpeed -= amount;
        isSlowing = true;

        yield return new WaitForSeconds(duration);

        moveSpeed += amount;
        isSlowing = false;  
    }

    private IEnumerator StunCoroutine(float duration)
    {
        isStunning = true;

        yield return new WaitForSeconds(duration);

        isStunning = false; 
    }

    private IEnumerator HitDamageCoroutine(float damage, float duration)
    {
        int dotDamage = (int)(damage / duration); 
        do //(duration > 0f)
        {
            TakeDamage(dotDamage);
             duration -= 1f;
            yield return new WaitForSeconds(1f);
        } while (duration > 0f);
    }

    #region Knockback
    public void GetKnockback(Vector2 dir, float power, float duration)
    {
        if (_isKnockbacking) return;
        _isKnockbacking = true;

        StartCoroutine(KnockBackCoroutine(dir, power, duration));

    }

    private IEnumerator KnockBackCoroutine(Vector2 dir, float power, float duration)
    {
        _rigid.AddForce(dir * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        ResetKnockBackParem();
    }

    private void ResetKnockBackParem()
    {
        //moveSpeed = 0;
        _rigid.velocity = Vector2.zero;
        _isKnockbacking = false;
    }
    #endregion
}
