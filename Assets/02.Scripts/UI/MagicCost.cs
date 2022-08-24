using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MagicCost : MonoBehaviour
{
    [SerializeField] private Image _gaugeTemp;
    [SerializeField] private float _generateTime = 1f;
    [SerializeField] private int _maxMagicCost = 10;
    [SerializeField] private float descreasedValue = 0.05f;
    private TMP_Text _magicCostText;
    public static int magic;


    private List<Image> _gaugeList = new List<Image>();

    private Coroutine _overMagicEffetCoroutine;

    private void Awake()
    {
        _magicCostText = transform.Find("MagicCostText").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        for (int i = 0; i < _maxMagicCost; i++)
        {
            Image gauge = Instantiate(_gaugeTemp, _gaugeTemp.transform.parent);
            _gaugeList.Add(gauge);
        }

        EventManager.StartListening("GameStart", Init);
        EventManager.StartListening("OverMagic", OverMagic);
    }

    private void OverMagic()
    {
        if (_overMagicEffetCoroutine != null)
        {
            _overMagicEffetCoroutine = null;
            StopCoroutine(_overMagicEffetCoroutine);
        }
        _overMagicEffetCoroutine = StartCoroutine(OverMagicCoroutine());
    }

    private IEnumerator OverMagicCoroutine()
    {
        transform.DOKill();
        transform.DOShakePosition(0.2f, 0.5f);
        _gaugeList.ForEach(x => x.color = Color.red);
        yield return new WaitForSeconds(0.2f);
        _gaugeList.ForEach(x => x.color = Color.white);

        _overMagicEffetCoroutine = null;
    }

    private void Init()
    {
        for (int i = 0; i < _gaugeList.Count; i++)
        {
            if (i < 5)
            {
                _gaugeList[i].transform.localScale = new Vector3(1f, 1f, 1f);
                _gaugeList[i].gameObject.SetActive(true);
            }

            else
            {
                _gaugeList[i].transform.localScale = new Vector3(0f, 1f, 1f);
                _gaugeList[i].gameObject.SetActive(false);

            }

        }

        magicCost();
    }

    private void magicCost()
    {
        magic = 5;
        StartCoroutine(magicUp(_generateTime));
    }
    IEnumerator magicUp(float time)
    {
        int timer = 0;

        while (true)
        {
            if (magic < 10)
            {
                if (magic != 10)
                {
                    _gaugeList[magic].gameObject.SetActive(true);
                    _gaugeList[magic].transform.DOScaleX(1f, time);
                }

                magic += 1;
            }

            yield return new WaitForSeconds(time);
            timer++;

            if (time > 0.5f && timer > 30)
            {
                time -= descreasedValue;
                timer = 0;
            }

            _magicCostText.text = magic.ToString();
        }
    }

    public void UseCost(int cost)
    {
        for (int i = 0; i < cost; i++)
        {
            Image gauge = _gaugeList[--magic];
            gauge.transform.localScale = new Vector3(0f, 1f, 1f);
            gauge.gameObject.SetActive(false);
        }
        _magicCostText.text = magic.ToString();
    }

    private void OnDestroy()
    {
        EventManager.StopListening("GameStart", Init);
    }
    private void OnApplicationQuit()
    {
        EventManager.StopListening("GameStart", Init);
    }


}
