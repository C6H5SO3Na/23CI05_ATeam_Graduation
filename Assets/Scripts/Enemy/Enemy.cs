using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyData
{
    //�ϐ�-------------------------------------------------------------------
    public EnemyStateMachine stateMachine { get; private set; }             // �X�e�[�g�}�V���̃C���X�^���X
    public IReceiveDeathInformation receiveInstance { get; private set; }   // ���S���������󂯎��֐������N���X�̃C���X�^���X
    public Transform player1Transform { get; private set; }                 // �v���C���[1��Transform���
    public Transform player2Transform { get; private set; }                 // �v���C���[2��Transform���

    //public int moveCount = 0;

    //�֐�-------------------------------------------------------------------
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
        else
        {
            Debug.LogWarning("��Ԃ��ݒ肳��Ă��܂���");
        }
    }

    /// <summary>
    /// ���̃X�N���v�g���A�^�b�`�����I�u�W�F�N�g�Ƃ͕ʂ̃I�u�W�F�N�g�ɂ���R���|�[�l���g�̎Q�Ɛ�̐ݒ�
    /// </summary>
    /// <param name="receiveInstance"> �G�����S���������󂯎��֐������������N���X </param>
    /// <param name="player1Transform"> �v���C���[1��Transform </param>
    /// <param name="player2Transform"> �v���C���[2��Transform </param>
    public void SetDependent(IReceiveDeathInformation receiveInstance, Transform player1Transform, Transform player2Transform)
    {
        //�G�����S���������󂯎��֐������������N���X�̐ݒ�
        if(receiveInstance != null)
        {
            this.receiveInstance = receiveInstance;
        }
        else
        {
            Debug.LogWarning("�G�����S���������󂯎��֐������������N���X������܂���");
        }

        //�v���C���[1�̈ʒu�����擾
        if (player1Transform != null)
        {
            this.player1Transform = player1Transform;
        }
        else
        {
            Debug.LogWarning("�v���C���[1�̈ʒu��񂪂���܂���");
        }

        //�v���C���[2�̈ʒu�����擾
        if (player2Transform != null)
        {
            this.player2Transform = player2Transform;
        }
        else
        {
            Debug.LogWarning("�v���C���[2�̈ʒu��񂪂���܂���");
        }
    }
}
