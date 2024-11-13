using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stage1EnemyStateBase : MonoBehaviour
{
    //関数
    /// <summary>
    /// 状態遷移の処理
    /// </summary>
    public abstract void StateTransition(Stage1EnemyStateBase nowState);

    /// <summary>
    /// 状態毎の行動処理
    /// </summary>
    public abstract void ActProcessingEachState(in Stage1EnemyStateBase nowState);
}
