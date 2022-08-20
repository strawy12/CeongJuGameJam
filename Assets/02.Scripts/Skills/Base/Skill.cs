using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : PoolableMono
{
    [SerializeField] protected LayerMask _enemyLayer;
    [SerializeField] protected SkillDataSO _skillData;

    public virtual void UsingSkill()
    {
        StartCoroutine(DetectCoroutine());
    }


    protected IEnumerator DetectCoroutine()
    {
        yield return new WaitForSeconds(_skillData.startDelay);

        if (_skillData.isImmediated)
        {
            Detect();
        }

        else
        {
            float timer = _skillData.skillDuration;

            while (timer >= 0f)
            {
                Detect();
                timer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        PoolManager.Inst.Push(this);
    }

    protected virtual void Detect()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _skillData.radius, _enemyLayer);
        foreach (var hit in hits)
        {
            AttackTarget(hit);
        }
    }


    private void AttackTarget(Collider2D hit)
    {
        IHittable hitAgent = hit.GetComponent<IHittable>();

        foreach (var ccEffect in _skillData.ccList)
        {
            if (hitAgent != null && ccEffect.ccType > ECrowdControlType.None)
            {
                hitAgent?.GetCrowdCtrl(ccEffect.ccType, ccEffect.ccAamout, ccEffect.ccDuration);
            }

            else if(hitAgent != null && ccEffect.ccType == ECrowdControlType.Attack)
            {
                hitAgent?.GetHit(ccEffect.ccAamout, gameObject, ccEffect.ccDuration);
            }

            else if (ccEffect.ccType == ECrowdControlType.Knockback)
            {
                IKnockback knockbackAgent = hit.GetComponent<IKnockback>();
                Vector2 dir = hit.transform.position - transform.position;

                knockbackAgent?.GetKnockback(dir.normalized, ccEffect.ccAamout, ccEffect.ccDuration);
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _skillData.radius);
    }
}
