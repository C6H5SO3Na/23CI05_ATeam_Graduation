using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    GameManager gameManager;    // �Q�[���}�l�[�W���[�̃C���X�^���X(Set�֐�����擾)
    bool isOn_Player1;          // �v���C���[1������Ă��邩
    bool isOn_Player2;          // �v���C���[2������Ă��邩

    void Start()
    {
        isOn_Player1 = false;
        isOn_Player2 = false;
    }

    //�S�[���u���b�N�̏�ɏ������N���A����
    void OnTriggerEnter(Collider other)
    {
        //�{�b�N�X�R���C�_�[�݂̂ɔ�������
        if (other is BoxCollider)
        {
            //������I�u�W�F�N�g���v���C���[������
            if (other.CompareTag("Player"))
            {
                //�v���C���[�̃C���X�^���X���擾����
                PlayerController player = other.GetComponent<PlayerController>();
                if (player)
                {
                    //�S�[���ɏ�����v���C���[��1��2�����肷��
                    if (player.PlayerNum == 1)
                    {
                        isOn_Player1 = true;
                    }
                    else if (player.PlayerNum == 2)
                    {
                        isOn_Player2 = true;
                    }
                    else
                    {
                        Debug.LogWarning("�v���C���[�ԍ��̐ݒ���ԈႦ�Ă��܂�");
                    }
                }

                //�v���C���[1�A2���������X�e�[�W���N���A����
                if (isOn_Player1 && isOn_Player2)
                {
                    if (gameManager)
                    {
                        gameManager.ReceiveClearInformation();
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //�{�b�N�X�R���C�_�[�݂̂ɔ�������
        if (other is BoxCollider)
        {
            //�������I�u�W�F�N�g���v���C���[������
            if (other.gameObject.CompareTag("Player"))
            {
                //�v���C���[�̃C���X�^���X���擾����
                PlayerController player = other.GetComponent<PlayerController>();
                if (player)
                {
                    //�S�[�����甲�����v���C���[��1��2�����肷��
                    if (player.PlayerNum == 1)
                    {
                        isOn_Player1 = false;
                    }
                    else if (player.PlayerNum == 2)
                    {
                        isOn_Player2 = false;
                    }
                    
                }
            }
        }
    }

    /// <summary>
    /// ���I�u�W�F�N�g�̃C���X�^���X���擾����
    /// </summary>
    /// <param name="gameManager"> GameManager�R���|�[�l���g </param>
    public void SetInstance(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
