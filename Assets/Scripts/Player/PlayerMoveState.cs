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
        if (Input.GetButton("Jump")) { player.ChangeState(new PlayerJumpState(player, velocity)); }
        if (leaveMove) { player.ChangeState(new PlayerIdleState(player)); }
    }

    public override void Move()
    {
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            var moveVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            velocity = new Vector3(moveVec.x, velocity.y, moveVec.y);
            //Debug.Log(velocity);
            float normalizedDir = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(0.0f, velocity.x + normalizedDir, 0.0f);
        }
        else
        {
            leaveMove = true;
        }
    }

    public override void MoveButton(InputAction.CallbackContext context)
    {
        //var moveVec = context.ReadValue<Vector2>().normalized;
        //velocity = new Vector3(moveVec.x, velocity.y, moveVec.y);
        //Debug.Log(velocity);
        //float normalizedDir = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        //player.transform.rotation = Quaternion.Euler(0.0f, velocity.x + normalizedDir, 0.0f);
        //if (!context.performed)
        //{
        //    leaveMove = true;
        //}
    }

    public override void JumpButton(InputAction.CallbackContext context)
    {
        //if (context.started) { isJump = true; }
    }

    public override void HoldButton(InputAction.CallbackContext context)
    {
    }
}
