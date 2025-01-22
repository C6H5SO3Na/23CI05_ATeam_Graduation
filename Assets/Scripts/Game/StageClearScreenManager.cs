using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.SceneManagement;

public class StageClearScreenManager : MonoBehaviour
{
    [SerializeField] GameObject fade;
    AudioSource bgm;
    AudioSource se;
    StageClearSound sound;

    Fade fadeInstance;
    Blinking textBlinking;
    float tookTime = 0f;
    enum Phase
    {
        BeforePressButton, PreFadeOut, FadeOut
    }

    Phase phase;

    void Start()
    {
        Application.targetFrameRate = 60;//�t���[�����[�g�Œ�
        Cursor.visible = false;         //�J�[�\��������
        sound = GetComponent<StageClearSound>();

        //�T�E���h�v���C���[
        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        se = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();

        textBlinking = transform.parent.GetChild(2).GetComponent<Blinking>();
    }
    void Update()
    {
        switch (phase)
        {
            case Phase.BeforePressButton:
                if (Input.GetButtonDown("Hold_P1"))
                {
                    se.PlayOneShot(sound.choiceSE);
                    textBlinking.blinkSpeed = 5f;
                    phase = Phase.PreFadeOut;
                }
                break;

            case Phase.PreFadeOut:
                tookTime += Time.unscaledDeltaTime;
                if (tookTime >= 1f)
                {
                    //�t�F�[�h�A�E�g
                    fadeInstance = Instantiate(fade, transform.parent).GetComponent<Fade>();
                    fadeInstance.StartFadeOut(1f);
                    phase = Phase.FadeOut;
                }
                break;

            case Phase.FadeOut:
                if (!fadeInstance.DoFade())
                {
                    bgm.Stop();
                    SceneManager.LoadScene("StageSelectScene");
                }
                break;
        }
    }
}
