using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolableMono, IKnockback, IHittable
{
    private Rigidbody2D _rigid = null;
    private SpriteRenderer _spriteRenderer;
    private bool _isKnockbacking;

    public float deathDuration;
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

    private bool isDead;
    private bool isSlowing;
    private bool isStunning;
    private bool isHeal;

    public HpBar hpBar;

    private List<ECrowdControlType> _givedCCEffectList = new List<ECrowdControlType>();
    private bool _isDamaged;

    private void Awake()
    {
        if (hpBar == null)
        {
            hpBar = GetComponentInChildren<HpBar>();
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        hpBar.SetMaxHealth(currentHp);

        EventManager.StartListening("GameOver", Die);
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        if (isStunning)
            return;
        if (_isKnockbacking)
            return;

        _rigid.MovePosition(_rigid.position + Vector2.down * (moveSpeed / 4) * Time.fixedDeltaTime);
    }


    private void TakeDamage(float damage)
    {
        if (isDead) return;
        currentHp -= damage;
        GenerateDamagePopup(damage);
        StartCoroutine(HitEffect());
        hpBar.SetHealth(currentHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void GenerateDamagePopup(float damage)
    {
        DamagePopup popup = PoolManager.Inst.Pop("DamagePopup") as DamagePopup;

        popup.Setup((int)damage, transform.position + (Vector3.up * 0.5f));
    }

    private IEnumerator HitEffect()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        _spriteRenderer.color = Color.white;
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
        hpBar.SetHealth(currentHp);
        moveSpeed = baseMoveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;
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
        if (isDead) return;
        if (_isDamaged) return;
        _isDamaged = true;
        StartCoroutine(HitDamageCoroutine(damage, duration));
    }

    public void GetCrowdCtrl(ECrowdControlType type, float amount, float duration)
    {
        if (isDead) return;
        switch (type)
        {
            case ECrowdControlType.Slow:
                // 스피드 줄이기
                if (isSlowing)
                    return;

                StartCoroutine(SlowCoroutine(amount, duration));
                break;
            case ECrowdControlType.Stun:
                if (isStunning) return;
                // 움직임 멈춤
                StartCoroutine(StunCoroutine(duration));
                break;
            case ECrowdControlType.Heal:
                // 데미지만 닳게 하기

                if (isHeal) return;
                isHeal = true;
                StartCoroutine(HealCoroutine(amount, duration));
                break;
        }

        _givedCCEffectList.Add(type);
    }

    private IEnumerator SlowCoroutine(float amount, float duration)
    {
        moveSpeed -= amount;
        if (moveSpeed < 0)
            moveSpeed = 0;
        isSlowing = true;

        yield return new WaitForSeconds(duration);

        moveSpeed += amount;
        isSlowing = false;
        _givedCCEffectList.Remove(ECrowdControlType.Slow);
    }

    private IEnumerator StunCoroutine(float duration)
    {

        isStunning = true;

        yield return new WaitForSeconds(duration);

        isStunning = false;
        _givedCCEffectList.Remove(ECrowdControlType.Stun);
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
        _isDamaged = false;
        isHeal = false;
    }
    private IEnumerator HealCoroutine(float damage, float duration)
    {
        int dotDamage = (int)(damage / duration);
        do //(duration > 0f)
        {
            TakeDamage(dotDamage);
            Utils.PlayerRef.GetHeal(dotDamage * 0.25f);
            duration -= 1f;
            yield return new WaitForSeconds(1f);
        } while (duration > 0f);
        _isDamaged = false;

        _givedCCEffectList.Remove(ECrowdControlType.Heal);
    }

    private void Die()
    {
        isDead = true;
        Sequence seq = DOTween.Sequence();
        seq.Append(_spriteRenderer.material.DOFloat(0, "_Dissolve", deathDuration));

        seq.AppendCallback(() => PoolManager.Inst.Push(this));
    }

    #region Knockback
    public void GetKnockback(Vector2 dir, float power, float duration)
    {
        if (isDead) return;
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

    public override void Reset()
    {
        if (hpBar == null)
        {
            hpBar = GetComponentInChildren<HpBar>();
        }
        _spriteRenderer ??= GetComponent<SpriteRenderer>();
        _rigid ??= GetComponent<Rigidbody2D>();

        InitValue();
        _spriteRenderer.color = Color.white;
        isDead = false;
        _spriteRenderer.material.SetFloat("_Dissolve", 1f);
    }


    private void OnApplicationQuit()
    {
        EventManager.StopListening("GameOver", Die);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("GameOver", Die);
    }
}
