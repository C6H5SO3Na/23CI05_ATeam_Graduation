using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// �v���C���[��~���
/// </summary>
public class PlayerIdleState : PlayerStateMachine
{
    bool toMove = false;
    bool isJump = false;
    //�R���X�g���N�^
    public PlayerIdleState(in PlayerController player)
    {
        this.player = player;
        toMove = false;
        isJump = false;
    }

    public override void Initialize()
    {

    }

    public override void Think()
    {
        if (isJump) { player.ChangeState(new PlayerJumpState(player, velocity)); }
        if (toMove) { player.ChangeState(new PlayerMoveState(player)); }
    }

    public override void Move()
    {
        //Debug.Log("Stop");
    }

    public override void MoveButton(InputAction.CallbackContext context)
    {
        toMove = true;
    }

    public override void JumpButton(InputAction.CallbackContext context)
    {
        if (context.started) { isJump = true; }
    }

    public override void HoldButton(InputAction.CallbackContext context)
    {
    }
}
