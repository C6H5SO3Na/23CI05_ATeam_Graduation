using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyData
{
    //変数-------------------------------------------------------------------
    public EnemyStateMachine stateMachine { get; private set; } // ステートマシンのインスタンス
    public IReceiveDeathInformation receiveInstance;            // 死亡した情報を受け取るオブジェクトのインスタンス(Inspecter)

    //public int moveCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //デバッグ用
        //Application.targetFrameRate = 60;

        //ステートマシンの取得
        stateMachine = GetComponent<EnemyStateMachine>();

        //初期状態の設定
        if(stateMachine)
        {
            stateMachine.ChangeState(new EnemyState_Idle(this));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //moveCount++;

        if (stateMachine.currentState != null)
        {
            //状態遷移
            stateMachine.currentState.StateTransition();

            //状態毎の行動処理
            stateMachine.currentState.ActProcess();
        }
    }
}
