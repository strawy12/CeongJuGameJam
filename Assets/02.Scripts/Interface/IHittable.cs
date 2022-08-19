using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public Vector3 HitPoint { get; }
    public void GetHit(float damage, GameObject damageDealer);
    public void GetCrowdCtrl(ECrowdControlType type, float amount, float duration);
}
