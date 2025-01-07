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
        Application.targetFrameRate = 60;//�t���[�����[�g�Œ�
        Cursor.visible = false;         //�J�[�\��������
        sound = GetComponent<TitleSound>();

        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        se = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();

        //BGM�Đ�
        bgm.clip = sound.titleBGM;
        bgm.Play();

        fadeInstance = Instantiate(fade, canvas.transform).GetComponent<Fade>();
        fadeInstance.StartFadeIn(1f);
        phase = Phase.FadeIn;
    }

    void Update()
    {
        //�i�K���Ƃɏ����𕪂���
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
                if(tookTime >= 1f)
                {
                    fadeInstance = Instantiate(fade, canvas.transform).GetComponent<Fade>();
                    fadeInstance.StartFadeOut(1f);
                    phase = Phase.FadeOut;
                }
                break;

            case Phase.FadeOut:

                if (!fadeInstance.DoFade())
                {
                    EndGame();
                }
                break;

        }
    }

    void Selecting()
    {
        //�C�[�W���O���͓��삵�Ȃ�
        if (DOTween.IsTweening(selectImage.transform)) { return; }
        //���ڑI��
        if (Mathf.Abs(Input.GetAxis("Vertical_P1")) > 0f && !DOTween.IsTweening(selectImage.transform))
        {
            se.PlayOneShot(sound.selectSE);
            selectImage.Move(Input.GetAxis("Vertical_P1"));
        }

        //B�Ō���
        if (Input.GetButtonDown("Submit"))
        {
            Choice();
        }
    }

    /// <summary>
    /// ���肵���Ƃ��̏���
    /// </summary>
    void Choice()
    {
        se.PlayOneShot(sound.choiceSE);
        switch (selectImage.SelectNum)
        {
            case (int)Select.SinglePlay:
                selectImage.ChangeBlinkSpeed(5f);
                GameManager.isMultiPlay = false;
                bgm.Stop();
                SceneManager.LoadScene("StageSelectScene");
                break;

            case (int)Select.MultiPlay:
                selectImage.ChangeBlinkSpeed(5f);
                GameManager.isMultiPlay = true;
                bgm.Stop();
                SceneManager.LoadScene("StageSelectScene");
                break;

            case (int)Select.Option:

                break;

            case (int)Select.Credit:
                //���łł͖�����
                break;

            case (int)Select.Exit:
                selectImage.ChangeBlinkSpeed(5f);
                phase = Phase.PreFadeOut;
                break;
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
