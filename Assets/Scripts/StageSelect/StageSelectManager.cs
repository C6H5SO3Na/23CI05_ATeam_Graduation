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

    Vector3 prePosition = Vector3.zero;
    int preSiblingIndex = 0;
    RectTransform targetImage;//�C�[�W���O������摜

    enum Phase
    {
        SelectStage, PreEntry, Entry, CanselEntry
    }
    Phase phase;

    void Update()
    {
        //�C�[�W���O���͓��삵�Ȃ�
        if (DOTween.IsTweening(selectFrame.transform)) { return; }

        switch (phase)
        {
            case Phase.SelectStage:
                //�^�C�g���֖߂�
                if (Input.GetButtonDown("Jump_P1"))
                {
                    SceneManager.LoadScene("TitleScene");
                }

                //�I��
                if (Input.GetButtonDown("Submit"))
                {
                    selectFrame.gameObject.SetActive(false);
                    ExpansionStageImage();
                    phase = Phase.PreEntry;
                }

                //�g�𓮂���
                if (Mathf.Abs(Input.GetAxis("Horizontal_P1")) != 0f || Mathf.Abs(Input.GetAxis("Vertical_P1")) != 0f)
                {
                    selectFrame.Move(Input.GetAxis("Horizontal_P1"), Input.GetAxis("Vertical_P1"));
                }
                break;


            case Phase.PreEntry:
                //�C�[�W���O���I���܂őҋ@
                if (!DOTween.IsTweening(targetImage.transform))
                {
                    phase = Phase.Entry;
                }
                break;

            case Phase.Entry:
                //�L�����Z��
                if (Input.GetButtonDown("Jump_P1"))
                {
                    ReductionStageImage();
                    phase = Phase.CanselEntry;
                }

                //����
                if (Input.GetButtonDown("Submit"))
                {
                    string stageName = canvas.transform.GetChild(canvas.transform.childCount - 1).GetChild(0).GetComponent<TextMeshProUGUI>().text;
                    //SceneManager.LoadScene("GameScene");
                    Debug.Log(stageName);
                }
                break;

            case Phase.CanselEntry:
                //�C�[�W���O���I���܂őҋ@
                if (!DOTween.IsTweening(targetImage.transform))
                {
                    selectFrame.gameObject.SetActive(true);
                    phase = Phase.SelectStage;
                    targetImage.SetSiblingIndex(preSiblingIndex);
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
    }
}
