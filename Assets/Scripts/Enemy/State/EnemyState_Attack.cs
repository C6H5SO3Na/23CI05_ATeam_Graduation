using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Attack : IEnemyStateBase
{
    //�R���X�g���N�^----------------------------------------------------------
    public EnemyState_Attack(Enemy stateOwner, AttackBase doAttack = null)
    {
        //�l�̐ݒ�
        this.stateOwner = stateOwner;
        isChangingIdle = false;
        this.doAttack = doAttack; 
    }

    //�ϐ�-------------------------------------------------------------------
    Enemy       stateOwner;     // ���̏�ԂɂȂ�N���X�̃C���X�^���X
    bool        isChangingIdle; // �ҋ@��ԂɕύX���邩
    AttackBase  doAttack;       // �s���U��

    //�֐�-------------------------------------------------------------------
    public void Enter()
    {
        //�ǂ̍U�����s�������܂��Ă��Ȃ������猈�߂�
        if(doAttack == null)
        {
            //�ǂ̍U���ɂ��邩�����_���Ō��߂�
            int index = Random.Range(0, stateOwner.attackTypes.Count);

            //�U����ݒ�
            doAttack = stateOwner.enemyAttacks[stateOwner.attackTypes[index]];
        }

        //�U���N���X�̏�����
        if(doAttack != null)
        {
            doAttack.Initialize(stateOwner.attackNoticeObjectGeneraterInstance, stateOwner);
        }
        else
        {
            Debug.LogWarning("�U�����ݒ肳��Ă��Ȃ�");
        }

        Debug.Log("Attack��ԊJ�n�I");
    }

    public void StateTransition()
    {
        //�ҋ@��Ԃ֑J��
        if(isChangingIdle)
        {
            stateOwner.stateMachine.ChangeState(new EnemyState_Idle(stateOwner));
        }

        //���S��Ԃ֑J��
        if(stateOwner.healthPoint <= 0)
        {
            stateOwner.stateMachine.ChangeState(new EnemyState_Die(stateOwner));
        }
    }

    public void ActProcess()
    {
        if(doAttack != null)
        {
            isChangingIdle = doAttack.Attack();
        }
    }

    public void Exit()
    {
        stateOwner.moveCount = 0;

        Debug.Log("Attack��ԏI���I");
    }
}
