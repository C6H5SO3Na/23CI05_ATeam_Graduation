using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Fade : MonoBehaviour
{
    Image fade; //Image
    Color fadeColor; //フェードしている際の色
    float time; //秒
    enum FadeMode
    {
        Neutral, FadeIn, FadeOut
    }
    FadeMode mode;//フェードの種類

    void Start()
    {
        //初期化
        fade = GetComponent<Image>();
        time = 1f; //デフォルトは1秒
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartFadeIn(1f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartFadeOut(1f);
        }
        //モードごとに処理を分ける
        switch (mode)
        {
            case FadeMode.Neutral:
                //空実装
                break;
            case FadeMode.FadeIn:
                FadeIn(time);
                break;
            case FadeMode.FadeOut:
                FadeOut(time);
                break;
        }
        fade.color = fadeColor;
    }
    /// <summary>
    /// フェードインの処理
    /// </summary>
    void FadeIn(float second)
    {
        fadeColor.a -= Time.deltaTime / time;
        if (fadeColor.a < 0f)
        {
            mode = FadeMode.Neutral;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// フェードアウトの処理
    /// </summary>
    void FadeOut(float second)
    {
        fadeColor.a += Time.deltaTime / time;
        if (fadeColor.a > 1f)
        {
            mode = FadeMode.Neutral;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// フェードイン開始
    /// </summary>
    /// <param name="second">所要秒数</param>
    public void StartFadeIn(float second)
    {
        mode = FadeMode.FadeIn;
        fadeColor = new Color(0f, 0f, 0f, 1f);
        time = second;
    }

    /// <summary>
    /// フェードアウト開始
    /// </summary>
    /// <param name="second">所要秒数</param>
    public void StartFadeOut(float second)
    {
        mode = FadeMode.FadeOut;
        fadeColor = new Color(0f, 0f, 0f, 0f);
        time = second;
    }

    public bool DoFade()
    {
        return mode != FadeMode.Neutral;
    }
}
