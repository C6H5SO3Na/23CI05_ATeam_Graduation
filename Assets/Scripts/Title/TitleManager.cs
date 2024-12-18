using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] SelectImageManager selectImage;
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource se;
    TitleSound sound;
    void Start()
    {
        Application.targetFrameRate = 60;//フレームレート固定
        sound = GetComponent<TitleSound>();
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
                case SelectImageManager.Select.SinglePlay:
                    GameManager.isMultiPlay = false;
                    SceneManager.LoadScene("StageSelectScene");
                    break;

                case SelectImageManager.Select.MultiPlay:
                    GameManager.isMultiPlay = true;
                    SceneManager.LoadScene("StageSelectScene");
                    break;

                case SelectImageManager.Select.Option:

                    break;

                case SelectImageManager.Select.Credit:
                    //β版では未実装
                    break;

                case SelectImageManager.Select.Exit:
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
