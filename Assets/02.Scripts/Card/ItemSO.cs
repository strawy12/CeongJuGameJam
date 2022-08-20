using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name; //카드이름
    public Sprite sprite;
    public int cost; //카드 코스트
    public float per;
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Item[] items;
}
