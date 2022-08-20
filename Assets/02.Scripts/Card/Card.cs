using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Card : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Image cardImage;
    [SerializeField] TMP_Text cardCost;
    [SerializeField] TMP_Text cardName;
    public int _cost;

    public Item item;

    public bool IsEmpty { get; private set; }

    private void Awake()
    {
        IsEmpty = true;
        gameObject.SetActive(false);
    }

    public void Setup(Item item)
    {
        gameObject.SetActive(true);

        this.item = item;
        cardImage.sprite = item.sprite;
        cardName.text = item.cardName;
        cardCost.text = item.cost.ToString();
        _cost = item.cost;

        IsEmpty = false;
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


    public void Release()
    {
        item = null;
        IsEmpty = true;
        gameObject.SetActive(false);
    }
}
