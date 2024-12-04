using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyState_Die : IEnemyStateBase
{
    //コンストラクタ----------------------------------------------------------
    public EnemyState_Die(Enemy stateOwner)
    {
        this.stateOwner = stateOwner;
    }

    //変数-------------------------------------------------------------------
    Enemy stateOwner;    // この状態になるクラスを保持する

    //関数-------------------------------------------------------------------
    public void Enter()
    {
        Debug.Log("Die状態開始！");
    }

    public void StateTransition()
    {
        
    }

    public void ActProcess()
    {
        //死亡(破棄)する
        Object.Destroy(stateOwner.gameObject);
    }

    public void Exit()
    {
        //stateOwner.moveCount = 0;

        Debug.Log("Die状態終了！");
    }
}
