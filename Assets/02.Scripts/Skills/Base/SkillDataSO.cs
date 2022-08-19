using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public float radius = 5f;
    public float damage = 1f;

    /// <summary>
    /// ����
    /// </summary>
    public float startDelay = 0f;

    /// <summary>
    /// �ĵ�
    /// </summary>
    public float afterDelay = 0f;

    /// <summary>
    /// cc�� Ÿ��
    /// </summary>
    public ECrowdControlType ccType;

    /// <summary>
    /// cc�� ���ӽð�
    /// </summary>
    public float ccDuration = 0f;

    /// <summary>
    /// cc�� ��ġ
    /// </summary>
    public float ccAamout = 0f;

    /// <summary>
    /// ����� ���� Ȯ��
    /// </summary>
    public bool isImmediated = false;

    /// <summary>
    /// ��ų ���� �ð�
    /// </summary>
    public float skillDuration = 0f;
}
