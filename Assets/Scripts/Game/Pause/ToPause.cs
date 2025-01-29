using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPause : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameManager gameManager;
    void Update()
    {
        //�|�[�Y�ɑJ�ڂ��Ȃ��������
        if (PauseScreenManager.IsPause) { return; }//�|�[�Y��ʂ����ɕ\������Ă���
        if (gameManager.isGameOver) { return; }//�Q�[���I�[�o�[��ʂ��o�Ă���
        if (gameManager.isClear) { return; }//�Q�[���N���A��ʂ��o�Ă���

        //1�R��
        if (Input.GetButtonDown("Pause_P1"))
        {
            GameObject instance = Instantiate(pauseScreen, transform.parent);
            instance.transform.GetChild(0).GetComponent<PauseScreenManager>().SetPause(1);
        }

        //2�R��
        if (Input.GetButtonDown("Pause_P2"))
        {
            GameObject instance = Instantiate(pauseScreen, transform.parent);
            instance.transform.GetChild(0).GetComponent<PauseScreenManager>().SetPause(2);
        }
    }
}
