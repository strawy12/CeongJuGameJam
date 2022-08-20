using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamagePopup : PoolableMono
{
    private TextMeshPro _textMesh;

    private void Awake()
    {
        
    }

    public void Setup(int damageAmount, Vector3 pos)
    {
        transform.position = pos;
        _textMesh.SetText(damageAmount.ToString());
        _textMesh.DOFade(1f, 0f);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(transform.position.y + 0.7f, 1f));
        seq.Join(_textMesh.DOFade(0f, 1f));
        seq.AppendCallback(() =>
        {
            PoolManager.Inst.Push(this);
        });
    }

    public override void Reset()
    {
        _textMesh ??= GetComponent<TextMeshPro>();
        _textMesh.color = Color.white;
        _textMesh.fontSize = 5f;
    }

}
