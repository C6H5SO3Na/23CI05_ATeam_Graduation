using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    //�ϐ�-------------------------------------------------------------------
    public IEnemyStateBase currentState;   // ���݂̏��

    //�֐�-------------------------------------------------------------------
    /// <summary>
    /// ��Ԃ̕ύX
    /// </summary>
    public void ChangeState(IEnemyStateBase newState)
    {
        if (currentState != null)
        {
            //�ύX�O�̏�ԏI�����ɍs�����������s
            currentState.Exit();
        }

        //��Ԃ̕ύX
        currentState = newState;

        //�ύX��̏�ԊJ�n���ɍs�����������s
        currentState.Enter();
    }
}
