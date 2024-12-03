using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状態基底インターフェイス
/// </summary>
public interface IEnemyStateBase
{
    //関数-------------------------------------------------------------------
    /// <summary>
    /// 状態開始時に行う処理
    /// </summary>
    void Enter();

    /// <summary>
    /// 状態遷移の処理
    /// </summary>
    void StateTransition();

    /// <summary>
    /// 行動処理(毎フレーム処理する)
    /// </summary>
    void ActProcess();

    /// <summary>
    /// 状態終了時に行う処理
    /// </summary>
    void Exit();
}
