using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ԋ��C���^�[�t�F�C�X
/// </summary>
public interface IEnemyStateBase
{
    //�֐�-------------------------------------------------------------------
    /// <summary>
    /// ��ԊJ�n���ɍs������
    /// </summary>
    void Enter();

    /// <summary>
    /// ��ԑJ�ڂ̏���
    /// </summary>
    void StateTransition();

    /// <summary>
    /// �s������(���t���[����������)
    /// </summary>
    void ActProcess();

    /// <summary>
    /// ��ԏI�����ɍs������
    /// </summary>
    void Exit();
}
