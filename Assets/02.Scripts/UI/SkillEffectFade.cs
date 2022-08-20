using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillEffectFade : MonoBehaviour
{
    [SerializeField] private float _duration;
    private Image _skillEffectImage;
    private bool _isShow;

    private void Awake()
    {
        _skillEffectImage = GetComponent<Image>();
    }

    public void Showeffect()
    {
        if (_isShow)
            return;
        _isShow = true;
        StartCoroutine(FadeEffect());
    }

    private IEnumerator FadeEffect()
    {
        _skillEffectImage.DOFade(1f, _duration);
        yield return new WaitForSeconds(_duration);
        _skillEffectImage.DOFade(0f, _duration);

        _isShow = false;
    }
}
