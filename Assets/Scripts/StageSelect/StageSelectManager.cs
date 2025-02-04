using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField] SelectFrameController selectFrame;
    [SerializeField] Canvas canvas;

    [SerializeField] float targetScale; // 目標スケール
    [SerializeField] float duration; // アニメーションの持続時間

    AudioSource bgm; //BGM用
    AudioSource se;  //SE用

    StageSelectSound sound;

    Vector3 prePosition = Vector3.zero;
    int preSiblingIndex = 0;
    RectTransform targetImage;//イージングさせる画像

    [SerializeField] GameObject fade;
    Fade fadeInstance;
    string nextSceneName;
    float tookTime = 0f;

    [SerializeField] TextMeshProUGUI operationText;

    enum Phase
    {
        FadeIn, SelectStage, PreEntry, Entry, CancelEntry, Cancel, StageChoice, FadeOut
    }
    Phase phase;

    void Start()
    {
        //BGMとSEそれぞれのオブジェクトを読み込み
        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        se = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();
        sound = GetComponent<StageSelectSound>();
        bgm.clip = sound.BGM;
        bgm.Play();

        fadeInstance = Instantiate(fade, transform.parent).GetComponent<Fade>();
        fadeInstance.StartFadeIn(1f);
    }

    void Update()
    {
        //イージング中は動作しない
        if (DOTween.IsTweening(selectFrame.transform)) { return; }

        switch (phase)
        {
            case Phase.FadeIn:
                operationText.text = "B:決定 A:タイトルへ戻る";
                //フェードし終わるまで
                if (!fadeInstance.DoFade())
                {
                    phase = Phase.SelectStage;
                }
                break;

            case Phase.SelectStage:
                operationText.text = "B:決定 A:タイトルへ戻る";
                //タイトルへ戻る
                if (Input.GetButtonDown("Jump_P1"))
                {
                    se.PlayOneShot(sound.cancelSE);
                    phase = Phase.Cancel;
                }

                //選択
                if (Input.GetButtonDown("Submit"))
                {
                    selectFrame.gameObject.SetActive(false);
                    ExpansionStageImage();
                    phase = Phase.PreEntry;
                    se.PlayOneShot(sound.enlargeSE);
                }

                //枠を動かす
                if (Mathf.Abs(Input.GetAxis("Horizontal_P1")) != 0f || Mathf.Abs(Input.GetAxis("Vertical_P1")) != 0f)
                {
                    selectFrame.Move(Input.GetAxis("Horizontal_P1"), Input.GetAxis("Vertical_P1"));
                    se.PlayOneShot(sound.selectSE);
                }
                break;


            case Phase.PreEntry:
                operationText.text = "";
                //イージングが終わるまで待機
                if (!DOTween.IsTweening(targetImage.transform))
                {
                    phase = Phase.Entry;
                }
                break;

            case Phase.Entry:
                operationText.text = "B:決定 A:戻る";
                //キャンセル
                if (Input.GetButtonDown("Jump_P1"))
                {
                    ReductionStageImage();
                    phase = Phase.CancelEntry;
                    se.PlayOneShot(sound.shrinkSE);
                }

                //決定
                if (Input.GetButtonDown("Submit"))
                {
                    AppManager.StageName = canvas.transform.GetChild(canvas.transform.childCount - 1).GetChild(0).GetComponent<TextMeshProUGUI>().text;
                    se.PlayOneShot(sound.choiceSE);
                    phase = Phase.StageChoice;
                }
                break;

            case Phase.CancelEntry:
                operationText.text = "";
                //イージングが終わるまで待機
                if (!DOTween.IsTweening(targetImage.transform))
                {
                    selectFrame.gameObject.SetActive(true);
                    phase = Phase.SelectStage;
                    targetImage.SetSiblingIndex(preSiblingIndex);
                }
                break;

            case Phase.Cancel:
                operationText.text = "";
                tookTime += Time.deltaTime;
                if (tookTime >= 1f)
                {
                    fadeInstance = Instantiate(fade, transform.parent).GetComponent<Fade>();
                    fadeInstance.StartFadeOut(1f);
                    nextSceneName = "TitleScene";
                    phase = Phase.FadeOut;
                }
                break;

            case Phase.StageChoice:
                operationText.text = "";
                tookTime += Time.deltaTime;
                if (tookTime >= 1f)
                {
                    fadeInstance = Instantiate(fade, transform.parent).GetComponent<Fade>();
                    fadeInstance.StartFadeOut(1f);
                    nextSceneName = "GameScene";
                    phase = Phase.FadeOut;
                }
                break;

            case Phase.FadeOut:
                operationText.text = "";
                if (!fadeInstance.DoFade())
                {
                    bgm.Stop();
                    SceneManager.LoadScene(nextSceneName);
                }
                break;
        }
    }

    /// <summary>
    /// ステージ画像を拡大
    /// </summary>
    void ExpansionStageImage()
    {
        //1-1～4-4までに対応
        targetImage = canvas.transform.GetChild(4 + selectFrame.SelectNumY * 4 + selectFrame.SelectNumX).GetComponent<RectTransform>();
        prePosition = targetImage.localPosition;
        targetImage.DOAnchorPos(Vector3.zero, duration).SetEase(Ease.OutQuad);
        targetImage.DOScale(new Vector3(targetScale, targetScale, targetScale), duration).SetEase(Ease.OutQuad).OnStart(() =>
        {
            preSiblingIndex = targetImage.GetSiblingIndex();
            targetImage.SetAsLastSibling();
        });

        targetImage.GetChild(0).GetComponent<Blinking>().enabled = true;
    }

    /// <summary>
    /// ステージ画像を縮小
    /// </summary>
    void ReductionStageImage()
    {
        //一番下
        targetImage = canvas.transform.GetChild(canvas.transform.childCount - 1).GetComponent<RectTransform>();

        targetImage.DOAnchorPos(prePosition, duration).SetEase(Ease.OutQuad);
        targetImage.DOScale(new Vector3(1f, 1f, 1f), duration).SetEase(Ease.OutQuad);

        Transform stageNum = targetImage.GetChild(0);
        stageNum.GetComponent<Blinking>().enabled = false;
        stageNum.GetComponent<TextMeshProUGUI>().color = Color.white;

    }
}
