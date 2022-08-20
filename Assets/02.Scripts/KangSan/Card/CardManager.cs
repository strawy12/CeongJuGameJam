using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }
    //private void Awake()
    //{
    //    Instance = this;
    //}
    void Awake() => Instance = this;

    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] List<Card> myCards;
    [SerializeField] Transform cardTransform; // 카드가 나오는 장소

    [SerializeField] TextMeshProUGUI magicCostTxt;

    List<Item> itemBuffer;
    Card selectCard = null;
    bool isMyCardDrag;
    bool onMyCardArea;
    private CardSoundManager soundManager;

    private int magic; // cost에 사용에 필요한 마법

    private GraphicRaycaster graphicRaycaster;

    private void magicCost()
    {
        StartCoroutine(magicUp(1f));
    }
    IEnumerator magicUp(float time)
    {
        while (true)
        {
            if (magic < 10)
                magic += 1;
            yield return new WaitForSeconds(time);
        }
    }

    public Item PopItem()
    {
        if (itemBuffer.Count == 0)
        {
            SetupItemBuffer();
        }
        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }

    void SetupItemBuffer()
    {
        itemBuffer = new List<Item>(100);
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            Item item = itemSO.items[i];
            for (int j = 0; j < item.per; j++)
                itemBuffer.Add(item);
        }
        for (int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }

    private void Start()
    {
        soundManager = GetComponent<CardSoundManager>();
        SetupItemBuffer();
        magicCost();
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        for (int i = 0; i < 4; i++)
        {
            AddCard(false);
        }
    }
    private void Update()
    {
        magicCostTxt.text = magic.ToString();
        if (isMyCardDrag)
        {
            CardDrag();
        }
        DetectCardArea();
    }

    private void CardDrag()
    {
        if (!onMyCardArea)
        {
            selectCard.transform.position = Utils.MousePos;

            //selectCard.MoveTransform(Vector3.one, false);
        }
    }
    void DetectCardArea()
    {
        List<RaycastResult> hits = new List<RaycastResult>();
        PointerEventData pointerEventData = new PointerEventData(null);
        pointerEventData.position = Input.mousePosition;
        graphicRaycaster.Raycast(pointerEventData, hits);
        onMyCardArea = false;
        foreach (var hit in hits)
        {
            if (hit.gameObject.CompareTag("asd"))
            {
                onMyCardArea = true;

                if (selectCard != null && isMyCardDrag == false)
                {
                    selectCard.transform.SetParent(cardTransform);
                }
            }

        }
    }

    void AddCard(bool useEffect = true)
    {

        var card = PoolManager.Inst.Pop("Card") as Card;
        card.transform.SetParent(cardTransform);
        card.Setup(PopItem());
        myCards.Add(card);

        card.transform.localScale = Vector3.zero;

        if (useEffect)
        {
            card.MoveTransform(Vector3.one, true, 0.75f);
            if(soundManager != null)
                soundManager.DrawCardSound();
        }

        else
        {
            card.MoveTransform(Vector3.one, false);
        }
    }
    #region MyCard

    public void CardMouseDown(Card card)
    {
        selectCard = card;
        isMyCardDrag = true;

        selectCard.transform.SetParent(cardTransform.parent);
    }
    public void CardMouseUp()
    {
        if (selectCard == null) return;
        int cost = selectCard._cost;
        if (cost > magic)
        {
            selectCard.transform.SetParent(cardTransform);
            selectCard = null;
            isMyCardDrag = false;
        }
        else
        {
            magic -= cost;
            if(soundManager != null)
                soundManager.UseCardSound();
            SpawnSkill(selectCard.item.name);

            selectCard.Release();
            selectCard = null;
            isMyCardDrag = false;
            AddCard(true);
        }
    }

    private void SpawnSkill(string cardName)
    {

    }

    #endregion

}
