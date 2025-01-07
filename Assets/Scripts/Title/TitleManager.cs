using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] SelectImageManager selectImage;
    AudioSource bgm;
    AudioSource se;
    TitleSound sound;
    [SerializeField] GameObject fade;
    [SerializeField] Canvas canvas;
    Fade fadeInstance;
    float tookTime = 0f;
    enum Select
    {
        SinglePlay = 0, MultiPlay, Option, Credit, Exit
    }

    enum Phase
    {
        FadeIn, Select, PreFadeOut, FadeOut
    }

    Phase phase;

    void Start()
    {
        Application.targetFrameRate = 60;//フレームレート固定
        Cursor.visible = false;         //カーソルを消す
        sound = GetComponent<TitleSound>();

        //サウンドプレイヤー
        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        se = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();

        //BGM再生
        bgm.clip = sound.titleBGM;
        bgm.Play();

        //フェードイン
        fadeInstance = Instantiate(fade, canvas.transform).GetComponent<Fade>();
        fadeInstance.StartFadeIn(1f);
        phase = Phase.FadeIn;
    }

    void Update()
    {
        //段階ごとに処理を分ける
        switch (phase)
        {
            case Phase.FadeIn:
                if (!fadeInstance.DoFade())
                {
                    phase = Phase.Select;
                }
                break;

            case Phase.Select:
                Selecting();
                break;

            case Phase.PreFadeOut:
                tookTime += Time.deltaTime;
                if (tookTime >= 1f)
                {
                    //フェードアウト
                    fadeInstance = Instantiate(fade, canvas.transform).GetComponent<Fade>();
                    fadeInstance.StartFadeOut(1f);
                    phase = Phase.FadeOut;
                }
                break;

            case Phase.FadeOut:

                if (!fadeInstance.DoFade())
                {
                    AfterFadeOut();
                }
                break;
        }
    }

    /// <summary>
    /// 選択しているときの処理
    /// </summary>
    void Selecting()
    {
        //イージング中は動作しない
        if (DOTween.IsTweening(selectImage.transform)) { return; }
        //項目選択
        if (Mathf.Abs(Input.GetAxis("Vertical_P1")) > 0f && !DOTween.IsTweening(selectImage.transform))
        {
            se.PlayOneShot(sound.selectSE);
            selectImage.Move(Input.GetAxis("Vertical_P1"));
        }

        //Bで決定
        if (Input.GetButtonDown("Submit"))
        {
            Choice();
        }
    }

    /// <summary>
    /// フェードアウト後の処理
    /// </summary>
    void AfterFadeOut()
    {
        switch (selectImage.SelectNum)
        {
            //シーン遷移は共通
            case (int)Select.SinglePlay:
            case (int)Select.MultiPlay:
                bgm.Stop();
                SceneManager.LoadScene("StageSelectScene");
                break;

            case (int)Select.Exit:
                EndGame();
                break;
        }
    }

    /// <summary>
    /// 決定したときの処理
    /// </summary>
    void Choice()
    {
        se.PlayOneShot(sound.choiceSE);
        switch (selectImage.SelectNum)
        {
            case (int)Select.SinglePlay:
                GameManager.isMultiPlay = false;
                selectImage.ChangeBlinkSpeed(5f);
                phase = Phase.PreFadeOut;
                break;

            case (int)Select.MultiPlay:
                GameManager.isMultiPlay = false;
                selectImage.ChangeBlinkSpeed(5f);
                phase = Phase.PreFadeOut;
                break;

            case (int)Select.Option:

                break;

            case (int)Select.Credit:
                //β版では未実装
                break;

            case (int)Select.Exit:
                selectImage.ChangeBlinkSpeed(5f);
                phase = Phase.PreFadeOut;
                break;
        }
    }

    /// <summary>
    /// アプリケーション終了
    /// </summary>
    void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
