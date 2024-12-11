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
        Application.targetFrameRate = 60;//�t���[�����[�g�Œ�
    }

    void Update()
    {
        //�Q�[���I��(��o�v��)
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EndGame();
        }

        //���ڑI��
        if (Mathf.Abs(Input.GetAxis("Vertical_P1")) > 0f && !DOTween.IsTweening(selectImage.transform))
        {
            selectImage.Move(Input.GetAxis("Vertical_P1"));
        }

        //B�Ō���
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
                    //���łł͖�����
                    break;

                case SelectImageManager.Select.Exit:
                    EndGame();
                    break;
            }
        }
    }

    /// <summary>
    /// �A�v���P�[�V�����I��
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
