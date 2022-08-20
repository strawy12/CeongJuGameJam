using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameQuit : MonoBehaviour
{
    private bool isClick = false;

    public TextMesh backBtnText;

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isClick)
                {
                    Application.Quit();
                }

                StartCoroutine(BackBtnClick());
            }
        }
    }

    IEnumerator BackBtnClick()
    {
        isClick = true;
        backBtnText.color = new Color(255, 255, 255, 1);

        StartCoroutine(Fade(1, 0, 1f));

        yield return new WaitForSeconds(1f);

        isClick = false;    
    }

    private IEnumerator Fade(float start, float end, float fadeTime)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = backBtnText.color;
            color.a = Mathf.Lerp(start, end, percent);
            backBtnText.color = color;

            yield return null;
        }
    }
}
