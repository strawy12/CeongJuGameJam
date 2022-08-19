using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IKnockback, IHittable
{
    private Rigidbody2D _rigid = null;
    private bool _isKnockbacking;

    public Vector3 HitPoint => throw new System.NotImplementedException();

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
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
                break;
            case ECrowdControlType.Stun:
                // 움직임 멈춤
                break;
            case ECrowdControlType.Heal:
                break;
        }
    }

    private IEnumerator HitDamageCoroutine(float damage, float duration)
    {
        do //(duration > 0f)
        {
            // 데미지 받음
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
