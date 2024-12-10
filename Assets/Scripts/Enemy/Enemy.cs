using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyData
{
    //変数-------------------------------------------------------------------
    public EnemyStateMachine stateMachine { get; private set; }             // ステートマシンのインスタンス
    public IReceiveDeathInformation receiveInstance { get; private set; }   // 死亡した情報を受け取る関数を持つクラスのインスタンス
    public Transform player1Transform { get; private set; }                 // プレイヤー1のTransform情報
    public Transform player2Transform { get; private set; }                 // プレイヤー2のTransform情報

    //public int moveCount = 0;

    //関数-------------------------------------------------------------------
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
        else
        {
            Debug.LogWarning("状態が設定されていません");
        }
    }

    /// <summary>
    /// このスクリプトがアタッチされるオブジェクトとは別のオブジェクトにあるコンポーネントの参照先の設定
    /// </summary>
    /// <param name="receiveInstance"> 敵が死亡した情報を受け取る関数を実装したクラス </param>
    /// <param name="player1Transform"> プレイヤー1のTransform </param>
    /// <param name="player2Transform"> プレイヤー2のTransform </param>
    public void SetDependent(IReceiveDeathInformation receiveInstance, Transform player1Transform, Transform player2Transform)
    {
        //敵が死亡した情報を受け取る関数を実装したクラスの設定
        if(receiveInstance != null)
        {
            this.receiveInstance = receiveInstance;
        }
        else
        {
            Debug.LogWarning("敵が死亡した情報を受け取る関数を実装したクラスがありません");
        }

        //プレイヤー1の位置情報を取得
        if (player1Transform != null)
        {
            this.player1Transform = player1Transform;
        }
        else
        {
            Debug.LogWarning("プレイヤー1の位置情報がありません");
        }

        //プレイヤー2の位置情報を取得
        if (player2Transform != null)
        {
            this.player2Transform = player2Transform;
        }
        else
        {
            Debug.LogWarning("プレイヤー2の位置情報がありません");
        }
    }
}
