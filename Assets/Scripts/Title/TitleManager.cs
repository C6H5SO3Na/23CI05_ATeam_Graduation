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
    enum Select
    {
        SinglePlay = 0, MultiPlay, Option, Credit, Exit
    }

    void Start()
    {
        Application.targetFrameRate = 60;//フレームレート固定
        sound = GetComponent<TitleSound>();

        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        se = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();

        bgm.clip = sound.titleBGM;
        bgm.Play();
    }

    void Update()
    {
        //項目選択
        if (Mathf.Abs(Input.GetAxis("Vertical_P1")) > 0f && !DOTween.IsTweening(selectImage.transform))
        {
            se.PlayOneShot(sound.selectSE);
            selectImage.Move(Input.GetAxis("Vertical_P1"));
        }

        //Bで決定
        if (Input.GetButtonDown("Submit"))
        {
            se.PlayOneShot(sound.choiceSE);
            switch (selectImage.SelectNum)
            {
                case (int)Select.SinglePlay:
                    GameManager.isMultiPlay = false;
                    bgm.Stop();
                    SceneManager.LoadScene("StageSelectScene");
                    break;

                case (int)Select.MultiPlay:
                    GameManager.isMultiPlay = true;
                    bgm.Stop();
                    SceneManager.LoadScene("StageSelectScene");
                    break;

                case (int)Select.Option:

                    break;

                case (int)Select.Credit:
                    //β版では未実装
                    break;

                case (int)Select.Exit:
                    EndGame();
                    break;
            }
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
