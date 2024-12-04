using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Attack : IEnemyStateBase
{
    //コンストラクタ----------------------------------------------------------
    public EnemyState_Attack(Enemy stateOwner)
    {
        this.stateOwner = stateOwner;
    }

    //変数-------------------------------------------------------------------
    Enemy stateOwner;    // この状態になるクラスを保持する

    //関数-------------------------------------------------------------------
    public void Enter()
    {
        Debug.Log("Attack状態開始！");
    }

    public void StateTransition()
    {

        //死亡状態へ遷移
        if(stateOwner.healthPoint <= 0)
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

        Debug.Log("Attack状態終了！");
    }
}
