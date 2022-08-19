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

    public void GetHit(float damage, GameObject damageDealer)
    {
    }

    public void GetCrowdCtrl(ECrowdControlType type, float amount, float duration)
    {
    }
}
