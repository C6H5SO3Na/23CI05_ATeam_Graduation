using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] SelectImageManager selectImage; //選択枠
    AudioSource bgm;  //BGM用
    AudioSource se;   //SE用
    TitleSound sound; //タイトルのサウンド一覧
    [SerializeField] GameObject fade; //フェード
    [SerializeField] Canvas canvas;   //キャンバス
    Fade fadeInstance; //フェードのインスタンスを代入
    float tookTime = 0f;
    enum Select
    {
        SinglePlay = 0, MultiPlay, Exit
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
            //フェードイン
            case Phase.FadeIn:
                if (!fadeInstance.DoFade())
                {
                    phase = Phase.Select;
                }
                break;

            //選択
            case Phase.Select:
                Selecting();
                break;

            //フェードアウト前の待機
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

            //フェードアウト中
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
            case (int)Select.SinglePlay: //シングルプレイ
            case (int)Select.MultiPlay:  //マルチプレイ
                bgm.Stop();
                SceneManager.LoadScene("StageSelectScene");
                break;

            case (int)Select.Exit:  //終わる
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
            //シングルプレイ
            case (int)Select.SinglePlay:
                GameManager.isMultiPlay = false;
                selectImage.ChangeBlinkSpeed(5f);
                phase = Phase.PreFadeOut;
                break;

            //マルチプレイ
            case (int)Select.MultiPlay:
                GameManager.isMultiPlay = true;
                selectImage.ChangeBlinkSpeed(5f);
                phase = Phase.PreFadeOut;
                break;

            //終わる
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
        //エディター上では
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //実行ファイル上では
        Application.Quit();
#endif
    }
}
