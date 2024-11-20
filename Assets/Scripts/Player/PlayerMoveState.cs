using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMoveState : PlayerStateMachine
{
    bool leaveMove = false;
    bool isJump = false;
    //コンストラクタ
    public PlayerMoveState(in PlayerController player)
    {
        this.player = player;
    }

    public override void Initialize()
    {

    }

    public override void Think()
    {
        if (isJump) { player.ChangeState(new PlayerJumpState(player, velocity)); }
        if (leaveMove) { player.ChangeState(new PlayerIdleState(player)); }
    }

    public override void Move()
    {
        //Debug.Log("Move");
        //Debug.Log(velocity);
    }

    public override void MoveButton(InputAction.CallbackContext context)
    {
        var moveVec = context.ReadValue<Vector2>();
        velocity = new Vector3(moveVec.x, velocity.y, moveVec.y);
        float normalizedDir = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0.0f, velocity.x + normalizedDir, 0.0f);
        if (context.canceled) { leaveMove = true; }
    }

    public override void JumpButton(InputAction.CallbackContext context)
    {
        if (context.started) { isJump = true; }
    }

    public override void HoldButton(InputAction.CallbackContext context)
    {
    }
}
