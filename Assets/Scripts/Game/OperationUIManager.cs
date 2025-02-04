using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OperationUIManager : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (GameManager.isMultiPlay)
        {
            text.text = "B:����/������\nA:�W�����v\nY:����L����\n�؂�ւ�\nX:�u��\n���X�e�B�b�N\n:�ړ�\nSTART:�|�[�Y";
        }
        else
        {
            text.text = "B:����/������\nA:�W�����v\nX:�u��\n���X�e�B�b�N\n:�ړ�\nSTART:�|�[�Y";
        }
    }
}
