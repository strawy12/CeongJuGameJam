using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Card : PoolableMono, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image cardImage;
    [SerializeField] TMP_Text cardCost;
    [SerializeField] TMP_Text cardName;
    public int _cost;

    public Item item;

    public void Setup(Item item)
    {
        cardImage.sprite = item.sprite;
        cardName.text = item.name;
        cardCost.text = item.cost.ToString();
        _cost = item.cost;
    }
    public void MoveTransform(Vector3 scale, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOScale(scale, dotweenTime);
        }
        else
        {
            transform.localScale = scale;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CardManager.Inst.CardMouseDown(this);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        CardManager.Inst.CardMouseUp();
    }

    public override void Reset()
    {
        transform.localScale = Vector3.zero;

        item = null;
    }

    public void Release()
    {
        Reset();
        PoolManager.Inst.Push(this);
    }
}
