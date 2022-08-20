using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class CardManager : MonoSingleton<CardManager>
{
    //private void Awake()
    //{
    //    Instance = this;
    //}

    [SerializeField] ItemSO itemSO;
    [SerializeField] List<Card> myCards;
    [SerializeField] Transform cardTransform; // 카드가 나오는 장소

    [SerializeField] private int[] defaultPercents;
    [SerializeField] private HoldCard _holdCardIamge;
    [SerializeField] private MagicCost _magicCost;

    Card selectCard = null;


    bool isMyCardDrag;
    bool onMyCardArea;

    bool isExitDetectArea = false;

    bool isPickGrade3Card;

    private GraphicRaycaster graphicRaycaster;


    private void Start()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();

        SetCardItemPercent(defaultPercents);

        for (int i = 0; i < 4; i++)
        {
            AddCard(false);
        }
    }
    private void Update()
    {
        if (isMyCardDrag)
        {
            CardDrag();
        }
        DetectCardArea();
    }

    private void SetCardItemPercent(int[] percents)
    {
        itemSO.items = itemSO.items.OrderBy(x => x.grade).ToArray();

        int beforeGrade = 1;
        int cardCnt = 0;
        int percent;
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            if (beforeGrade != itemSO.items[i].grade || i == itemSO.items.Length - 1)
            {
                percent = GetCardGradePercent(beforeGrade, percents);

                beforeGrade = itemSO.items[i].grade;


                for (int j = i - cardCnt; j < i; j++)
                {
                    itemSO.items[j].percent = percent / cardCnt;
                }

                cardCnt = 0;
            }

            cardCnt++;
        }

        percent = GetCardGradePercent(beforeGrade, percents);

        int cnt = itemSO.items.Length - 1;
        for (int i = 0; i < cardCnt; i++)
        {
            itemSO.items[cnt--].percent = percent / cardCnt;
        }
    } 

    private int GetCardGradePercent(int grade, int[] percents)
    {
        return percents[grade - 1];
    }

    public Item PopItem()
    {
        Item item = GetRandomItem();

        if (item.grade <= 3)
        {
            isPickGrade3Card = true;
        }

        return item;
    }

    private Item GetRandomItem()
    {
        if (isPickGrade3Card)
        {
            if (myCards.TrueForAll(x => x.item != null && x.item.grade <= 3))
            {
                isPickGrade3Card = false;
                int[] percents = defaultPercents;

                percents[0] = (int)(percents[0] * 1.5f);
                percents[1] = (int)(percents[1] * 1.5f);

                int overPercent = percents.Sum() - 100;

                if (percents[3] < overPercent * 0.5f)
                {
                    overPercent -= percents[3];
                    percents[3] = 0;
                    percents[2] -= overPercent;
                }

                else
                {
                    percents[2] -= (int)(overPercent * 0.5f);
                    percents[3] -= (int)(overPercent * 0.5f);
                }

                SetCardItemPercent(percents);
            }
        }

        int num = Random.Range(0, 100) + 1;

        for (int i = 0; i < itemSO.items.Length; i++)
        {
            int percent = itemSO.items[i].percent;

            if(num - percent <= 0)
            {
                return itemSO.items[i];
            }

            else
            {
                num -= percent;
            }
        }

        return null;
    }


    private void CardDrag()
    {
        if (!onMyCardArea)
        {
            _holdCardIamge.transform.position = Utils.MousePos;

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

                if (selectCard != null && isMyCardDrag && isExitDetectArea)
                {
                    selectCard.gameObject.SetActive(true);
                    ReleaseCard();
                }
            }

        }
    }

    void AddCard(bool useEffect = true)
    {
        Card card = null;
        for(int i =0; i < myCards.Count; i++)
        {
            if(myCards[i].IsEmpty)
            {
                card = myCards[i];
                break;
            }
        }

        Item item = PopItem();
        card.Setup(item);

        card.transform.localScale = Vector3.zero;

        if (useEffect)
        {
            card.MoveTransform(Vector3.one, true, 0.75f);
        }

        else
        {
            card.MoveTransform(Vector3.one, false);
        }
    }

    public void SetIsExitDetectArea(bool isExit)
    {
        isExitDetectArea = isExit;
    }
    #region MyCard

    public void CardMouseDown(Card card)
    {
        selectCard = card;
        selectCard.gameObject.SetActive(false);
        _holdCardIamge.transform.position = Utils.MousePos;
        _holdCardIamge.gameObject.SetActive(true);
        _holdCardIamge.sprite = selectCard.item.sprite;
        isMyCardDrag = true;
        isExitDetectArea = false;
    }
    public void CardMouseUp()
    {
        if (selectCard == null) return;

        int cost = selectCard._cost;
        if (cost > MagicCost.magic)
        {
            _holdCardIamge.gameObject.SetActive(false);
            selectCard.gameObject.SetActive(true);
            selectCard = null;
            isMyCardDrag = false;
        }
        else
        {
            _magicCost.UseCost(cost);
            SpawnSkill(selectCard.item.skillName);
            selectCard.Release();
            ReleaseCard();
            AddCard(true);
        }
    }

    public void ReleaseCard()
    {
        _holdCardIamge.gameObject.SetActive(false);
        selectCard = null;
        isMyCardDrag = false;
    }

    private void SpawnSkill(string cardName)
    {
        Skill skill = PoolManager.Inst.Pop(cardName) as Skill;

        if (skill != null)
        {
            skill.transform.position = Utils.MousePos;
            skill.UsingSkill();
        }
    }

    #endregion

}
