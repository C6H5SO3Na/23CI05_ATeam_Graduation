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

    [SerializeField] float targetScale; // �ڕW�X�P�[��
    [SerializeField] float duration; // �A�j���[�V�����̎�������

    AudioSource bgm; //BGM�p
    AudioSource se;  //SE�p

    StageSelectSound sound;

    Vector3 prePosition = Vector3.zero;
    int preSiblingIndex = 0;
    RectTransform targetImage;//�C�[�W���O������摜

    [SerializeField] GameObject fade;
    Fade fadeInstance;
    string nextSceneName;
    float tookTime = 0f;

    [SerializeField] TextMeshProUGUI operationText;

    enum Phase
    {
        FadeIn, SelectStage, PreEntry, Entry, CancelEntry, Cancel, StageChoice, FadeOut
    }
    Phase phase;

    void Start()
    {
        //BGM��SE���ꂼ��̃I�u�W�F�N�g��ǂݍ���
        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        se = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();
        sound = GetComponent<StageSelectSound>();
        bgm.clip = sound.BGM;
        bgm.Play();

        fadeInstance = Instantiate(fade, transform.parent).GetComponent<Fade>();
        fadeInstance.StartFadeIn(1f);
    }

    void Update()
    {
        //�C�[�W���O���͓��삵�Ȃ�
        if (DOTween.IsTweening(selectFrame.transform)) { return; }

        switch (phase)
        {
            case Phase.FadeIn:
                operationText.text = "B:���� A:�^�C�g���֖߂�";
                //�t�F�[�h���I���܂�
                if (!fadeInstance.DoFade())
                {
                    phase = Phase.SelectStage;
                }
                break;

            case Phase.SelectStage:
                operationText.text = "B:���� A:�^�C�g���֖߂�";
                //�^�C�g���֖߂�
                if (Input.GetButtonDown("Jump_P1"))
                {
                    se.PlayOneShot(sound.cancelSE);
                    phase = Phase.Cancel;
                }

                //�I��
                if (Input.GetButtonDown("Submit"))
                {
                    selectFrame.gameObject.SetActive(false);
                    ExpansionStageImage();
                    phase = Phase.PreEntry;
                    se.PlayOneShot(sound.enlargeSE);
                }

                //�g�𓮂���
                if (Mathf.Abs(Input.GetAxis("Horizontal_P1")) != 0f || Mathf.Abs(Input.GetAxis("Vertical_P1")) != 0f)
                {
                    selectFrame.Move(Input.GetAxis("Horizontal_P1"), Input.GetAxis("Vertical_P1"));
                    se.PlayOneShot(sound.selectSE);
                }
                break;


            case Phase.PreEntry:
                operationText.text = "";
                //�C�[�W���O���I���܂őҋ@
                if (!DOTween.IsTweening(targetImage.transform))
                {
                    phase = Phase.Entry;
                }
                break;

            case Phase.Entry:
                operationText.text = "B:���� A:�߂�";
                //�L�����Z��
                if (Input.GetButtonDown("Jump_P1"))
                {
                    ReductionStageImage();
                    phase = Phase.CancelEntry;
                    se.PlayOneShot(sound.shrinkSE);
                }

                //����
                if (Input.GetButtonDown("Submit"))
                {
                    AppManager.StageName = canvas.transform.GetChild(canvas.transform.childCount - 1).GetChild(0).GetComponent<TextMeshProUGUI>().text;
                    se.PlayOneShot(sound.choiceSE);
                    phase = Phase.StageChoice;
                }
                break;

            case Phase.CancelEntry:
                operationText.text = "";
                //�C�[�W���O���I���܂őҋ@
                if (!DOTween.IsTweening(targetImage.transform))
                {
                    selectFrame.gameObject.SetActive(true);
                    phase = Phase.SelectStage;
                    targetImage.SetSiblingIndex(preSiblingIndex);
                }
                break;

            case Phase.Cancel:
                operationText.text = "";
                tookTime += Time.deltaTime;
                if (tookTime >= 1f)
                {
                    fadeInstance = Instantiate(fade, transform.parent).GetComponent<Fade>();
                    fadeInstance.StartFadeOut(1f);
                    nextSceneName = "TitleScene";
                    phase = Phase.FadeOut;
                }
                break;

            case Phase.StageChoice:
                operationText.text = "";
                tookTime += Time.deltaTime;
                if (tookTime >= 1f)
                {
                    fadeInstance = Instantiate(fade, transform.parent).GetComponent<Fade>();
                    fadeInstance.StartFadeOut(1f);
                    nextSceneName = "GameScene";
                    phase = Phase.FadeOut;
                }
                break;

            case Phase.FadeOut:
                operationText.text = "";
                if (!fadeInstance.DoFade())
                {
                    bgm.Stop();
                    SceneManager.LoadScene(nextSceneName);
                }
                break;
        }
    }

    /// <summary>
    /// �X�e�[�W�摜���g��
    /// </summary>
    void ExpansionStageImage()
    {
        //1-1�`4-4�܂łɑΉ�
        targetImage = canvas.transform.GetChild(4 + selectFrame.SelectNumY * 4 + selectFrame.SelectNumX).GetComponent<RectTransform>();
        prePosition = targetImage.localPosition;
        targetImage.DOAnchorPos(Vector3.zero, duration).SetEase(Ease.OutQuad);
        targetImage.DOScale(new Vector3(targetScale, targetScale, targetScale), duration).SetEase(Ease.OutQuad).OnStart(() =>
        {
            preSiblingIndex = targetImage.GetSiblingIndex();
            targetImage.SetAsLastSibling();
        });

        targetImage.GetChild(0).GetComponent<Blinking>().enabled = true;
    }

    /// <summary>
    /// �X�e�[�W�摜���k��
    /// </summary>
    void ReductionStageImage()
    {
        //��ԉ�
        targetImage = canvas.transform.GetChild(canvas.transform.childCount - 1).GetComponent<RectTransform>();

        targetImage.DOAnchorPos(prePosition, duration).SetEase(Ease.OutQuad);
        targetImage.DOScale(new Vector3(1f, 1f, 1f), duration).SetEase(Ease.OutQuad);

        Transform stageNum = targetImage.GetChild(0);
        stageNum.GetComponent<Blinking>().enabled = false;
        stageNum.GetComponent<TextMeshProUGUI>().color = Color.white;

    }
}
