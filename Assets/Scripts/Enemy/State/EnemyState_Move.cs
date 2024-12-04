using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Move : IEnemyStateBase
{
    //�R���X�g���N�^----------------------------------------------------------
    public EnemyState_Move(Enemy stateOwner)
    {
        this.stateOwner = stateOwner;
    }

    //�ϐ�-------------------------------------------------------------------
    Enemy stateOwner;    // ���̏�ԂɂȂ�N���X��ێ�����

    //�֐�-------------------------------------------------------------------
    public void Enter()
    {
        Debug.Log("Move��ԊJ�n�I");
    }

    public void StateTransition()
    {

        //���S��Ԃ֑J��
        if (stateOwner.healthPoint <= 0)
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

        Debug.Log("Move��ԏI���I");
    }
}
