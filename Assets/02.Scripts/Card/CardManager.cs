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
    [SerializeField] Transform cardTransform; // Ä«µå°¡ ³ª¿À´Â Àå¼Ò

    [SerializeField] private int[] defaultPercents;
    [SerializeField] private HoldCard _holdCardIamge;
    [SerializeField] private MagicCost _magicCost;

    Card selectCard = null;
    CardSoundManager soundManager = null;

    bool isMyCardDrag;
    bool onMyCardArea;

    bool ignoreVampire = true;

    bool isExitDetectArea = false;

    bool isPickGrade3Card;

    private GraphicRaycaster graphicRaycaster;


    private IEnumerator Start()
    {
        //Utils.SetCardPercentDict();

        graphicRaycaster = GetComponent<GraphicRaycaster>();
        soundManager = GetComponent<CardSoundManager>();

        EventManager.StartListening("GameOver", Release);
        EventManager.StartListening("GameStart", Init);
        yield return new WaitForEndOfFrame();

        gameObject.SetActive(false);
        GetComponent<CanvasGroup>().alpha = 1f;
    }
    private void Update()
    {
        if (isMyCardDrag)
        {
            CardDrag();
        }
        DetectCardArea();
    }

    private IEnumerator IgnoreTimer()
    {
        yield return new WaitForSeconds(120f);
        ignoreVampire = false;
    }

    private void SetCardItemPercent(int[] percents)
    {
        itemSO.items = itemSO.items.OrderBy(x => x.grade).ToArray();

        int beforeGrade = 1;
        int cardCnt = 0;
        int percent;
        bool ignore = false;
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            if (beforeGrade != itemSO.items[i].grade)
            {
                percent = GetCardGradePercent(beforeGrade, percents);

                beforeGrade = itemSO.items[i].grade;

                if (ignoreVampire)
                    ignore = (itemSO.items[i - 1].grade == 1);

                for (int j = i - cardCnt; j < i; j++)
                {
                    if (ignore)
                    {
                        if (itemSO.items[j].cardName.Equals("ÈíÇ÷"))
                        {
                            itemSO.items[j].percent = 0;
                        }

                        else
                        {
                            itemSO.items[j].percent = percent / (cardCnt - 1);
                        }
                    }
                    else
                    {
                        itemSO.items[j].percent = percent / cardCnt;
                    }

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

        switch(item.grade)
        {
            case 1:
                SetCardItemPercent(new int[] { 0, 10, 30, 60 });
                break;
            case 2:
                SetCardItemPercent(new int[] { 5, 15, 30, 50 });
                break;
            case 3:
                SetCardItemPercent(new int[] { 20, 20, 30, 30 });
                break;
            case 4:
                SetCardItemPercent(new int[] { 20, 30, 30, 20 });
                break;
        }
        return item;
    }

    private Item GetRandomItem()
    {
        int num = Random.Range(0, 100) + 1;

        for (int i = 0; i < itemSO.items.Length; i++)
        {
            int percent = itemSO.items[i].percent;

            if (num - percent <= 0)
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
        for (int i = 0; i < myCards.Count; i++)
        {
            if (myCards[i].IsEmpty)
            {
                card = myCards[i];
                break;
            }
        }

        Item item = PopItem();
        card.Setup(item);
        soundManager.DrawCardSound();
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
        if (selectCard != null) return;

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
        if (onMyCardArea)
        {
            selectCard.gameObject.SetActive(true);
            ReleaseCard();
            return;
        }
        if (selectCard == null) return;

        int cost = selectCard._cost;
        if (cost > MagicCost.magic)
        {
            _holdCardIamge.gameObject.SetActive(false);
            selectCard.gameObject.SetActive(true);
            selectCard = null;
            isMyCardDrag = false;
            EventManager.TriggerEvent("OverMagic");
        }
        else
        {
            _magicCost.UseCost(cost);
            PoolSoundPlayer useCardSoundManager = PoolManager.Inst.Pop("CardUseSound") as PoolSoundPlayer;
            if (selectCard.clip != null)
                useCardSoundManager.UseSound(selectCard.clip);
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

    public void Release()
    {
        myCards.ForEach(x => x.Release());
    }

    public void Init()
    {
        ignoreVampire = true;
        StartCoroutine(IgnoreTimer());
        SetCardItemPercent(defaultPercents);

        for (int i = 0; i < 4; i++)
        {
            AddCard(false);
        }
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("GameStart", Release);
        EventManager.StopListening("GameStart", Init);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("GameStart", Release);
        EventManager.StopListening("GameStart", Init);
    }

}
