using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string cardName; //ī���̸�
    public string skillName; //��ų�̸�
    public Sprite sprite;
    public int cost; //ī�� �ڽ�Ʈ
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
