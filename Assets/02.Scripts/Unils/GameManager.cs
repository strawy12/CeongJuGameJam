using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PoolingListSO _poolingList;

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

}
