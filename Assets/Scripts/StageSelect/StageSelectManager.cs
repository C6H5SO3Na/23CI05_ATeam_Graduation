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

    AudioSource bgm;
    AudioSource se;

    StageSelectSound sound;

    Vector3 prePosition = Vector3.zero;
    int preSiblingIndex = 0;
    RectTransform targetImage;//イージングさせる画像

    enum Phase
    {
        SelectStage, PreEntry, Entry, CancelEntry
    }
    Phase phase;

    void Start()
    {
        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        se = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();
        sound = GetComponent<StageSelectSound>();
        bgm.clip = sound.BGM;
        bgm.Play();
    }

    void Update()
    {
        //イージング中は動作しない
        if (DOTween.IsTweening(selectFrame.transform)) { return; }

        switch (phase)
        {
            case Phase.SelectStage:
                //タイトルへ戻る
                if (Input.GetButtonDown("Jump_P1"))
                {
                    se.PlayOneShot(sound.cancelSE);
                    bgm.Stop();
                    SceneManager.LoadScene("TitleScene");
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
                //イージングが終わるまで待機
                if (!DOTween.IsTweening(targetImage.transform))
                {
                    phase = Phase.Entry;
                    se.PlayOneShot(sound.shrinkSE);
                }
                break;

            case Phase.Entry:
                //キャンセル
                if (Input.GetButtonDown("Jump_P1"))
                {
                    ReductionStageImage();
                    phase = Phase.CancelEntry;
                }

                //決定
                if (Input.GetButtonDown("Submit"))
                {
                    string stageName = canvas.transform.GetChild(canvas.transform.childCount - 1).GetChild(0).GetComponent<TextMeshProUGUI>().text;
                    //SceneManager.LoadScene("GameScene");
                    Debug.Log(stageName);
                }
                break;

            case Phase.CancelEntry:
                //イージングが終わるまで待機
                if (!DOTween.IsTweening(targetImage.transform))
                {
                    selectFrame.gameObject.SetActive(true);
                    phase = Phase.SelectStage;
                    targetImage.SetSiblingIndex(preSiblingIndex);
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
    }
}
