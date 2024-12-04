using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyData
{
    //�ϐ�-------------------------------------------------------------------
    public EnemyStateMachine stateMachine { get; private set; } // �X�e�[�g�}�V���̃C���X�^���X
    public IReceiveDeathInformation receiveInstance;            // ���S���������󂯎��I�u�W�F�N�g�̃C���X�^���X(Inspecter)

    //public int moveCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //�f�o�b�O�p
        //Application.targetFrameRate = 60;

        //�X�e�[�g�}�V���̎擾
        stateMachine = GetComponent<EnemyStateMachine>();

        //������Ԃ̐ݒ�
        if(stateMachine)
        {
            stateMachine.ChangeState(new EnemyState_Idle(this));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //moveCount++;

        if (stateMachine.currentState != null)
        {
            //��ԑJ��
            stateMachine.currentState.StateTransition();

            //��Ԗ��̍s������
            stateMachine.currentState.ActProcess();
        }
    }
}
