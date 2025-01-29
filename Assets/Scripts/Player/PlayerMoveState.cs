using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// プレイヤー歩行状態
/// </summary>
public class PlayerMoveState : PlayerStateMachine
{
    bool leaveMove = false;
    //bool isJump = false;//未使用
    //コンストラクタ
    public PlayerMoveState()
    {

    }

    public override void Initialize(PlayerController player)
    {

    }

    public override void Think(PlayerController player)
    {
        //持っているときだとジャンプできない
        if (Input.GetButtonDown("Jump" + player.PlayerName) && !player.IsHolding)
        {
            player.ChangeState(new PlayerJumpState());
        }

        if (leaveMove)
        {
            player.ChangeState(new PlayerIdleState());
        }
        //クリア
        if (player.gameManager.isClear)
        {
            player.ChangeState(new PlayerClearState());
        }
        //死亡
        if (player.gameManager.isGameOver)
        {
            player.ChangeState(new PlayerDeadState());
        }
    }

    public override void Move(PlayerController player)
    {
        //入力後
        if (player.IsInputStick())
        {
            player.Walk();
        }
        else
        {
            leaveMove = true;
        }
    }
}
