using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stage1EnemyStateBase : MonoBehaviour
{
    //�֐�
    /// <summary>
    /// ��ԑJ�ڂ̏���
    /// </summary>
    public abstract void StateTransition(Stage1EnemyStateBase nowState);

    /// <summary>
    /// ��Ԗ��̍s������
    /// </summary>
    public abstract void ActProcessingEachState(in Stage1EnemyStateBase nowState);
}
