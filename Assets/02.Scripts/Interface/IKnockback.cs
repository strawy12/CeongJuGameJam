using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockback
{
    public void GetKnockback(Vector2 dir, float power, float duration);
}
