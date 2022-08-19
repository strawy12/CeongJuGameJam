using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public float radius = 5f;
    public float damage = 1f;

    /// <summary>
    /// 선딜
    /// </summary>
    public float startDelay = 0f;

    /// <summary>
    /// 후딜
    /// </summary>
    public float afterDelay = 0f;

    /// <summary>
    /// cc기 타입
    /// </summary>
    public ECrowdControlType ccType;

    /// <summary>
    /// cc기 지속시간
    /// </summary>
    public float ccDuration = 0f;

    /// <summary>
    /// cc기 수치
    /// </summary>
    public float ccAamout = 0f;

    /// <summary>
    /// 즉발형 여부 확인
    /// </summary>
    public bool isImmediated = false;

    /// <summary>
    /// 스킬 지속 시간
    /// </summary>
    public float skillDuration = 0f;
}
