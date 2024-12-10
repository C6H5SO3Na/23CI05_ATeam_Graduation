using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Attack : IEnemyStateBase
{
    //�R���X�g���N�^----------------------------------------------------------
    public EnemyState_Attack(Enemy stateOwner)
    {
        this.stateOwner = stateOwner;
        isChangingIdle = false;
    }

    //�ϐ�-------------------------------------------------------------------
    Enemy stateOwner;       // ���̏�ԂɂȂ�N���X�̃C���X�^���X
    bool  isChangingIdle;   // �ҋ@��ԂɕύX���邩

    //�֐�-------------------------------------------------------------------
    public void Enter()
    {
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

    }

    public void Exit()
    {
        //stateOwner.moveCount = 0;

        Debug.Log("Attack��ԏI���I");
    }
}
