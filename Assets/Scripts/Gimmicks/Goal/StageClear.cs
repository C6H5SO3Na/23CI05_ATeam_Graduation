using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;    // �Q�[���}�l�[�W���[�̃C���X�^���X(Inspactor����擾)
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
        //������I�u�W�F�N�g���v���C���[������
        if (other.gameObject.CompareTag("Player"))
        {
            //�}���`�v���C�̏ꍇ
            if (GameManager.isMultiPlay)
            {
                //�v���C���[�̃C���X�^���X���擾����
                PlayerController�@player = other.GetComponent<PlayerController>();
                if(player)
                {
                    //�S�[���ɏ�����v���C���[��1��2�����肷��
                    if (player.playerNum == 1)
                    {
                        isOn_Player1 = true;
                    }
                    else if(player.playerNum == 2)
                    {
                        isOn_Player2 = true;
                    }
                    else
                    {
                        Debug.LogWarning("�v���C���[�ԍ��̐ݒ���ԈႦ�Ă��܂�");
                    }
                }

                //�v���C���[1�A2���������N���A�ɂ���
                if(isOn_Player1 && isOn_Player2)
                {
                    gameManager.ReceiveClearInformation();
                }
            }
            //�V���O���v���C�̏ꍇ
            else
            {
                //�N���A�ɂ���
                gameManager.ReceiveClearInformation();
            }
        }
    }
}
