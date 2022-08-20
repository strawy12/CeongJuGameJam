using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string cardName; //카드이름
    public string skillName; //스킬이름
    public Sprite sprite;
    public int cost; //카드 코스트
    public int grade;
    public int percent;
    public AudioClip skillSound;

    public bool ignoreItem;
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Item[] items;
}
