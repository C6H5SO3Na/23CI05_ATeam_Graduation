using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Attack : IEnemyStateBase
{
    //コンストラクタ----------------------------------------------------------
    public EnemyState_Attack(Enemy stateOwner, AttackBase doAttack = null)
    {
        //値の設定
        this.stateOwner = stateOwner;
        isChangingIdle = false;
        this.doAttack = doAttack; 
    }

    //変数-------------------------------------------------------------------
    Enemy       stateOwner;     // この状態になるクラスのインスタンス
    bool        isChangingIdle; // 待機状態に変更するか
    AttackBase  doAttack;       // 行う攻撃

    //関数-------------------------------------------------------------------
    public void Enter()
    {
        //どの攻撃を行うか決まっていなかったら決める
        if(doAttack == null)
        {
            //どの攻撃にするかランダムで決める
            int index = Random.Range(0, stateOwner.attackTypes.Count);

            //攻撃を設定
            doAttack = stateOwner.enemyAttacks[stateOwner.attackTypes[index]];
        }

        //攻撃クラスの初期化
        if(doAttack != null)
        {
            doAttack.Initialize(stateOwner.attackNoticeObjectGeneraterInstance, stateOwner);
        }
        else
        {
            Debug.LogWarning("攻撃が設定されていない");
        }

        Debug.Log("Attack状態開始！");
    }

    public void StateTransition()
    {
        //待機状態へ遷移
        if(isChangingIdle)
        {
            stateOwner.stateMachine.ChangeState(new EnemyState_Idle(stateOwner));
        }

        //死亡状態へ遷移
        if(stateOwner.healthPoint <= 0)
        {
            stateOwner.stateMachine.ChangeState(new EnemyState_Die(stateOwner));
        }
    }

    public void ActProcess()
    {
        if(doAttack != null)
        {
            isChangingIdle = doAttack.Attack();
        }
    }

    public void Exit()
    {
        stateOwner.moveCount = 0;

        Debug.Log("Attack状態終了！");
    }
}
