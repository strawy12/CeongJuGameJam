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

    }

    protected virtual void Detect()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _skillData.radius, _enemyLayer);
        Debug.Log(hits.Length);
        foreach (var hit in hits)
        {
            AttackTarget(hit);
        }
    }


    private void AttackTarget(Collider2D hit)
    {
        IHittable hitAgent = hit.GetComponent<IHittable>();

        if (hitAgent != null&& _skillData.damage > 0f)
        {
            hitAgent?.GetHit(_skillData.damage, gameObject);
        }

        if (hitAgent != null && _skillData.ccType > ECrowdControlType.Knockback)
        {
            hitAgent?.GetCrowdCtrl(_skillData.ccType, _skillData.ccAamout, _skillData.ccDuration);
        }

        if (_skillData.ccType == ECrowdControlType.Knockback)
        {
            IKnockback knockbackAgent = hit.GetComponent<IKnockback>();
            Vector2 dir = hit.transform.position - transform.position;

            knockbackAgent?.GetKnockback(dir.normalized, _skillData.ccAamout, _skillData.ccDuration);
        }

        // IHittable ÀÌ¿כ
    }
}
