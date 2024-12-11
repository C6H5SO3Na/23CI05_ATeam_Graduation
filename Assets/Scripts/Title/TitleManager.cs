using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] SelectImageManager selectImage;
    void Start()
    {
        Application.targetFrameRate = 60;//フレームレート固定
    }

    void Update()
    {
        //ゲーム終了(提出要件)
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EndGame();
        }

        //項目選択
        if (Mathf.Abs(Input.GetAxis("Vertical_P1")) > 0f && !DOTween.IsTweening(selectImage.transform))
        {
            selectImage.Move(Input.GetAxis("Vertical_P1"));
        }

        //Bで決定
        if (Input.GetButtonDown("Submit"))
        {
            switch(selectImage.SelectNum)
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
