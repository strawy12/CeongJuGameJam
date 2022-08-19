using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] Image cardImage;
    [SerializeField] TMP_Text cardCost;
    [SerializeField] TMP_Text cardName;
    public int _cost;

    public Item item;
    public PS originPS;

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
        CardManager.Instance.CardMouseDown(this);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        CardManager.Instance.CardMouseUp();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //CardManager.Instance.CardMouseExit(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
       // CardManager.Instance.CardMouseOver(this);
    }
}
