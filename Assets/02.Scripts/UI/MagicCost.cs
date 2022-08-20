using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class MagicCost : MonoBehaviour
{
    [SerializeField] private GameObject _gaugeTemp;
    [SerializeField] private float _generateTime = 1f;
    [SerializeField] private int _maxMagicCost = 10;
    private TMP_Text _magicCostText;
    public static int magic;


    private List<GameObject> _gaugeList = new List<GameObject>();

    private void Awake()
    {
        _magicCostText = transform.Find("MagicCostText").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        for (int i = 0; i < _maxMagicCost; i++)
        {
            GameObject gauge = Instantiate(_gaugeTemp, _gaugeTemp.transform.parent);
            gauge.transform.localScale = new Vector3(0f, 1f, 1f);
            gauge.SetActive(false);
            _gaugeList.Add(gauge);
        }
        magicCost();
    }

    private void magicCost()
    {
        magic = 0;
        StartCoroutine(magicUp(_generateTime));
    }
    IEnumerator magicUp(float time)
    {
        while (true)
        {
            if (magic < 10)
            {
                if (magic != 10)
                {
                    _gaugeList[magic].SetActive(true);
                    _gaugeList[magic].transform.DOScaleX(1f, time);
                }

                magic += 1;
            }

            yield return new WaitForSeconds(time);
            _magicCostText.text = magic.ToString();
        }
    }

    public void UseCost(int cost)
    {
        for(int i =0; i < cost; i++)
        {
            GameObject gauge = _gaugeList[--magic];
            gauge.transform.localScale = new Vector3(0f, 1f, 1f);
            gauge.SetActive(false);
        }
        _magicCostText.text = magic.ToString();
    }



}
