using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] SelectImageManager selectImage; //�I��g
    AudioSource bgm;  //BGM�p
    AudioSource se;   //SE�p
    TitleSound sound; //�^�C�g���̃T�E���h�ꗗ
    [SerializeField] GameObject fade; //�t�F�[�h
    [SerializeField] Canvas canvas;   //�L�����o�X
    Fade fadeInstance; //�t�F�[�h�̃C���X�^���X����
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
        Application.targetFrameRate = 60;//�t���[�����[�g�Œ�
        Cursor.visible = false;         //�J�[�\��������
        sound = GetComponent<TitleSound>();

        //�T�E���h�v���C���[
        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        se = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();

        //BGM�Đ�
        bgm.clip = sound.titleBGM;
        bgm.Play();

        //�t�F�[�h�C��
        fadeInstance = Instantiate(fade, canvas.transform).GetComponent<Fade>();
        fadeInstance.StartFadeIn(1f);
        phase = Phase.FadeIn;
    }

    void Update()
    {
        //�i�K���Ƃɏ����𕪂���
        switch (phase)
        {
            //�t�F�[�h�C��
            case Phase.FadeIn:
                if (!fadeInstance.DoFade())
                {
                    phase = Phase.Select;
                }
                break;

            //�I��
            case Phase.Select:
                Selecting();
                break;

            //�t�F�[�h�A�E�g�O�̑ҋ@
            case Phase.PreFadeOut:
                tookTime += Time.deltaTime;
                if (tookTime >= 1f)
                {
                    //�t�F�[�h�A�E�g
                    fadeInstance = Instantiate(fade, canvas.transform).GetComponent<Fade>();
                    fadeInstance.StartFadeOut(1f);
                    phase = Phase.FadeOut;
                }
                break;

            //�t�F�[�h�A�E�g��
            case Phase.FadeOut:

                if (!fadeInstance.DoFade())
                {
                    AfterFadeOut();
                }
                break;
        }
    }

    /// <summary>
    /// �I�����Ă���Ƃ��̏���
    /// </summary>
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
    /// �t�F�[�h�A�E�g��̏���
    /// </summary>
    void AfterFadeOut()
    {
        switch (selectImage.SelectNum)
        {
            //�V�[���J�ڂ͋���
            case (int)Select.SinglePlay: //�V���O���v���C
            case (int)Select.MultiPlay:  //�}���`�v���C
                bgm.Stop();
                SceneManager.LoadScene("StageSelectScene");
                break;

            case (int)Select.Exit:  //�I���
                EndGame();
                break;
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
            //�V���O���v���C
            case (int)Select.SinglePlay:
                GameManager.isMultiPlay = false;
                selectImage.ChangeBlinkSpeed(5f);
                phase = Phase.PreFadeOut;
                break;

            //�}���`�v���C
            case (int)Select.MultiPlay:
                GameManager.isMultiPlay = true;
                selectImage.ChangeBlinkSpeed(5f);
                phase = Phase.PreFadeOut;
                break;

            //�I���
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
        //�G�f�B�^�[��ł�
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //���s�t�@�C����ł�
        Application.Quit();
#endif
    }
}
