using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// プレイヤー停止状態
/// </summary>
public class PlayerIdleState : PlayerStateMachine
{
    //bool toMove = false;
    //bool isJump = false;
    bool isHold = false;
    //コンストラクタ
    public PlayerIdleState(in PlayerController player)
    {
        this.player = player;
        //toMove = false;
        //isJump = false;
    }

    public override void Initialize()
    {

    }

    public override void Think()
    {
        if (Input.GetButtonDown("Jump")) { player.ChangeState(new PlayerJumpState(player, velocity)); }
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) { player.ChangeState(new PlayerMoveState(player)); }
    }

    public override void Move()
    {
        //Debug.Log("Stop");
    }

    /*α版では未使用(InputSystem)
    public override void MoveButton(InputAction.CallbackContext context)
    {
        player.ChangeState(new PlayerMoveState(player));
    }

    public override void JumpButton(InputAction.CallbackContext context)
    {
        if (context.started) { isJump = true; }
    }

    public override void HoldButton(InputAction.CallbackContext context)
    {
    }
    */
}
