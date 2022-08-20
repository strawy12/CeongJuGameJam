using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PS
{
    public Vector3 pos;
    public Vector3 scale;

    public PS(Vector3 pos, Vector3 scale)
    {
        this.pos = pos;
        this.scale = scale;
    }
}

public class Utils : MonoBehaviour
{
    public static Quaternion QI => Quaternion.identity;

    public static Vector3 MousePos
    {
        get
        {
            Vector3 result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            result.z = 0;
            return result;
        }
    }
}
