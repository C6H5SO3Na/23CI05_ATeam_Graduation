using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Fade : MonoBehaviour
{
    Image fade; //Image
    Color fadeColor; //�t�F�[�h���Ă���ۂ̐F
    float time; //�b
    enum FadeMode
    {
        Neutral, FadeIn, FadeOut
    }
    FadeMode mode;//�t�F�[�h�̎��

    void Start()
    {
        //������
        fade = GetComponent<Image>();
        time = 1f; //�f�t�H���g��1�b
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartFadeIn(1f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartFadeOut(1f);
        }
        //���[�h���Ƃɏ����𕪂���
        switch (mode)
        {
            case FadeMode.Neutral:
                //�����
                break;
            case FadeMode.FadeIn:
                FadeIn();
                break;
            case FadeMode.FadeOut:
                FadeOut();
                break;
        }
        fade.color = fadeColor;
    }
    /// <summary>
    /// �t�F�[�h�C���̏���
    /// </summary>
    void FadeIn()
    {
        float deltaTime = Time.unscaledDeltaTime;
        if (deltaTime >= 0.5f) { return; }//�t���[�����[�g���Ⴗ����
        fadeColor.a -= deltaTime / time;
        if (fadeColor.a < -0f)
        {
            mode = FadeMode.Neutral;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �t�F�[�h�A�E�g�̏���
    /// </summary>
    void FadeOut()
    {
        float deltaTime = Time.unscaledDeltaTime;
        if (deltaTime >= 0.5f) { return; }//�t���[�����[�g���Ⴗ����
        fadeColor.a += deltaTime / time;
        if (fadeColor.a > 1f)
        {
            mode = FadeMode.Neutral;
        }
    }

    /// <summary>
    /// �t�F�[�h�C���J�n
    /// </summary>
    /// <param name="second">���v�b��</param>
    public void StartFadeIn(float second)
    {
        mode = FadeMode.FadeIn;
        fadeColor = new Color(0f, 0f, 0f, 1f);
        time = second;
    }

    /// <summary>
    /// �t�F�[�h�A�E�g�J�n
    /// </summary>
    /// <param name="second">���v�b��</param>
    public void StartFadeOut(float second)
    {
        mode = FadeMode.FadeOut;
        fadeColor = new Color(0f, 0f, 0f, 0f);
        time = second;
    }

    public bool DoFade()
    {
        return mode != FadeMode.Neutral;
    }
}
