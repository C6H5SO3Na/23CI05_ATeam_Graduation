using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    //�ϐ�-------------------------------------------------------------------
    IEnemyStateBase currentState;   // ���݂̏��

    //�֐�-------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if(currentState != null)
        {
            //��ԑJ��
            currentState.StateTransition();

            //�s������
            currentState.ActProcess();
        }
    }

    /// <summary>
    /// ��Ԃ̕ύX
    /// </summary>
    public void ChangeState(IEnemyStateBase newState)
    {
        if(currentState != null)
        {
            //�ύX�O�̏�ԏI�����ɍs�����������s
            currentState.Exit();

            //��Ԃ̕ύX
            currentState = newState;

            //�ύX��̏�ԊJ�n���ɍs�����������s
            currentState.Enter();
        }
    }
}
