using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
/// <summary>
/// プレイヤージャンプ状態
/// </summary>
public class PlayerJumpState : PlayerStateMachine
{

    //コンストラクタ
    public PlayerJumpState()
    {
        
    }

    public override void Initialize(PlayerController player)
    {
        //ジャンプする
        player.sound.PlayOneShot(player.SE.jumpSE);
        var jumpVec = new Vector3(player.GetInputDirection().x, 3f, player.GetInputDirection().z);
        player.UpdateMoveDirection(jumpVec);
    }

    public override void Think(PlayerController player)
    {
        //地面に着地したら遷移
        if (player.GetComponent<CharacterController>().isGrounded)
        {
            player.ChangeState(player.PreState);
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
        //移動
        if (player.IsInputStick())
        {
            player.Walk();
        }
        else
        {
            player.Deceleration(0.9f);
        }
    }
}
