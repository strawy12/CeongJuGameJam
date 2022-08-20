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

public class Utils
{
    public static Dictionary<int, int[]> cardPercentDict { get; private set; }

    public static void SetCardPercentDict()
    {
        cardPercentDict = new Dictionary<int, int[]>();
        cardPercentDict.Add(1,new int[] { 0, 10, 30, 60 });
        cardPercentDict.Add(2,new int[] { 5, 15, 30, 50 });
        cardPercentDict.Add(3,new int[] { 20, 20, 30, 30 });
        cardPercentDict.Add(4,new int[] { 20, 30, 30, 20 });
    }


    private static Player _playerRef;
    public static Player PlayerRef
    {
        get
        {
            if (_playerRef == null)
            {
                _playerRef = GameObject.FindObjectOfType<Player>();
            }

            return _playerRef;
        }
    }

    private static Camera _mainCam;
    public static Camera MainCam
    {
        get
        {
            if (_mainCam == null)
            {
                _mainCam = Camera.main;
            }

            return _mainCam;
        }
    }

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
