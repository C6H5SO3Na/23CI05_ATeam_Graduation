using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// プレイヤー停止状態
/// </summary>
public class PlayerIdleState : PlayerStateMachine
{
    //bool toMove = false;
    //bool isJump = false;

    //コンストラクタ
    public PlayerIdleState()
    {
        //toMove = false;
        //isJump = false;
    }

    public override void Initialize(PlayerController player)
    {

    }

    public override void Think(PlayerController player)
    {
        if (Input.GetButtonDown("Jump" + player.PlayerName) && !player.IsHolding)
        {
            player.ChangeState(new PlayerJumpState());
        }
        if (player.IsInputStick())
        {
            player.ChangeState(new PlayerMoveState());
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
        //重力は働かせる
        player.UpdateMoveDirection(new Vector3(0f, player.GetMoveDirection().y, 0f));
    }
}
