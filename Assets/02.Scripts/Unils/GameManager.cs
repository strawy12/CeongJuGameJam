using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PoolingListSO _poolingList;
    [SerializeField] private SkillEffectFade _bloodEffect;
    [SerializeField] private SkillEffectFade _blizardEffect;


    private void Awake()
    {
        new PoolManager(transform);

        InitPoolList();
    }

    private void InitPoolList()
    {
        foreach (var pair in _poolingList.list)
        {
            PoolManager.Inst.CreatePool(pair.prefab, pair.poolCnt);
        }
    }

    public void StartBlizardEffect()
    {
        _blizardEffect.Showeffect();
    }
    public void StartBloodEffect()
    {
        _bloodEffect.Showeffect();
    }
}
