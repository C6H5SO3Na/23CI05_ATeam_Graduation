using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] SelectImageManager selectImage;
    void Start()
    {
        Application.targetFrameRate = 60;//�t���[�����[�g�Œ�
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EndGame();
        }

        if (Mathf.Abs(Input.GetAxis("Vertical_P1")) > 0f && !DOTween.IsTweening(selectImage.transform))
        {
            selectImage.Move(Input.GetAxis("Vertical_P1"));
        }

        if (Input.GetButtonDown("Submit"))
        {
            switch(selectImage.SelectNum)
            {
                case SelectImageManager.Select.SinglePlay:

                    break;
                case SelectImageManager.Select.MultiPlay:

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
