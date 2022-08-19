using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PoolingList")]
public class PoolingListSO : ScriptableObject
{
    public List<PoolingPair> list;
}
