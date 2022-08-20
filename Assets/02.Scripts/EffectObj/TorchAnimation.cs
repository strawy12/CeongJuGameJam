using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TorchAnimation : MonoBehaviour
{
    [SerializeField] private bool _changeRadius; 

    [SerializeField] private float _intensityRandomness;
    [SerializeField] private float _radiusRandomness;
    [SerializeField] private float _timeRandomness;

    private float _baseIntensity;
    private float _baseRadius;
    private float _baseTime = 1f;

    private UnityEngine.Rendering.Universal.Light2D _light;

    private void Awake()
    {
        _light = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        _baseIntensity = _light.intensity;
        _baseRadius = _light.pointLightOuterRadius;
    }

    private void OnEnable()
    {
        ShakeLight();
    }

    private void ShakeLight()
    {
        if (!gameObject.activeSelf)
            return;

        float targetIntensity = _baseIntensity + Random.Range(-_intensityRandomness, _intensityRandomness);
        float targetTime = _baseTime + Random.Range(-_timeRandomness, _timeRandomness);

        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(
            () => _light.intensity,
            value => _light.intensity = value,
            targetIntensity,
            targetTime
        ));

        if (_changeRadius)
        {
            float targetRadius = _baseRadius + Random.Range(-_radiusRandomness, _radiusRandomness);

            seq.Append(DOTween.To(
                () => _light.pointLightOuterRadius,
                value => _light.pointLightOuterRadius = value,
                targetRadius,
                targetTime
            ));
        }
        

        

        seq.AppendCallback(() => ShakeLight());
    }
}
