using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Idle : IEnemyStateBase
{
    //コンストラクタ----------------------------------------------------------
    public EnemyState_Idle(Enemy stateOwner)
    {
        this.stateOwner = stateOwner;
    }

    //変数-------------------------------------------------------------------
    Enemy stateOwner;    // この状態になるクラスを保持する

    //関数-------------------------------------------------------------------
    public void Enter()
    {
        Debug.Log("Idle状態開始！");
    }

    public void StateTransition() 
    {

        //死亡状態へ遷移
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

        Debug.Log("Idle状態終了！");
    }
}
