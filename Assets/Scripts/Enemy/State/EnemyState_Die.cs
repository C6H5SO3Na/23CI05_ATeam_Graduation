using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyState_Die : IEnemyStateBase
{
    //�R���X�g���N�^----------------------------------------------------------
    public EnemyState_Die(Enemy stateOwner)
    {
        this.stateOwner = stateOwner;
    }

    //�ϐ�-------------------------------------------------------------------
    Enemy stateOwner;    // ���̏�ԂɂȂ�N���X��ێ�����

    //�֐�-------------------------------------------------------------------
    public void Enter()
    {
        Debug.Log("Die��ԊJ�n�I");
    }

    public void StateTransition()
    {
        
    }

    public void ActProcess()
    {
        //���S(�j��)����
        Object.Destroy(stateOwner.gameObject);
    }

    public void Exit()
    {
        //stateOwner.moveCount = 0;

        Debug.Log("Die��ԏI���I");
    }
}
