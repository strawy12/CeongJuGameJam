using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCamera : MonoBehaviour
{
    private static Camera _main;

    public static Camera Main
    {
        get
        {
            if (_main == null)
            {
                _main = Camera.main;
                _defaultMainCamPos = _main.transform.position;
            }

            return _main;
        }
    }

    private static Vector3 _defaultMainCamPos;

    public static void ShakeCamera(float duration, float strength = 3, int vibrato = 10, float randomNess = 90, bool fadeOut = true)
    {
        Main.DOKill(true);
        if (Main.transform.position != _defaultMainCamPos)
        {
            Main.transform.position = _defaultMainCamPos;
        }

        Main.DOShakePosition(duration, strength, vibrato, randomNess, fadeOut);
    }
}
