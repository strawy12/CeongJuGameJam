using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class HoldCard : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent OnClickUp;
    private Image _cardImage;

    public Sprite sprite
    {
        get 
        { 
            if(_cardImage == null)
            {
                _cardImage = GetComponent<Image>();
            }

            return _cardImage.sprite; 
        } 
        
        set
        {
            if (_cardImage == null)
            {
                _cardImage = GetComponent<Image>();
            }

            _cardImage.sprite = value;
        }
    }

    private bool _isEnter;

    private void Update()
    {
        if(_isEnter && Input.GetMouseButtonUp(0))
        {
            _isEnter = false;
            OnClickUp?.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isEnter = true;
    }
}
