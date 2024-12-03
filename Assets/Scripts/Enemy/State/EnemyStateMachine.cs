using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    //変数-------------------------------------------------------------------
    IEnemyStateBase currentState;   // 現在の状態

    //関数-------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if(currentState != null)
        {
            //状態遷移
            currentState.StateTransition();

            //行動処理
            currentState.ActProcess();
        }
    }

    /// <summary>
    /// 状態の変更
    /// </summary>
    public void ChangeState(IEnemyStateBase newState)
    {
        if(currentState != null)
        {
            //変更前の状態終了時に行う処理を実行
            currentState.Exit();

            //状態の変更
            currentState = newState;

            //変更後の状態開始時に行う処理を実行
            currentState.Enter();
        }
    }
}
