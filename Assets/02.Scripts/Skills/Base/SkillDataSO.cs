using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CrowdCtrl
{
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
}


[CreateAssetMenu(menuName = "SO/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public string cardName;
    public float radius = 5f;

    /// <summary>
    /// ����
    /// </summary>
    public float startDelay = 0f;

    /// <summary>
    /// �ĵ�
    /// </summary>
    public float afterDelay = 0f;

    public List<CrowdCtrl> ccList;

    /// <summary>
    /// ����� ���� Ȯ��
    /// </summary>
    public bool isImmediated = false;

    /// <summary>
    /// ��ų ���� �ð�
    /// </summary>
    public float skillDuration = 0f;
}
