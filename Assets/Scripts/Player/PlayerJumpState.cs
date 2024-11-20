using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// プレイヤー停止状態
/// </summary>
public class PlayerJumpState : PlayerStateMachine
{

    //コンストラクタ
    public PlayerJumpState(in PlayerController player, Vector3 vec)
    {
        this.player = player;
        velocity += new Vector3(vec.x, 11f, vec.z);
    }

    public override void Initialize()
    {

    }

    public override void Think()
    {
        if (player.GetComponent<CharacterController>().isGrounded)
        {
            if (player.preState is PlayerMoveState)
            {
                player.ChangeState(new PlayerMoveState(player));
            }
            else if (player.preState is PlayerIdleState)
            {
                player.ChangeState(new PlayerIdleState(player));
            }
        }
    }

    public override void Move()
    {
        //Debug.Log("Jump");
        //Debug.Log(velocity);
    }

    public override void MoveButton(InputAction.CallbackContext context)
    {
        var moveVec = context.ReadValue<Vector2>();
        velocity = new Vector3(moveVec.x, velocity.y, moveVec.y);
        float normalizedDir = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0.0f, velocity.x + normalizedDir, 0.0f);
    }

    public override void JumpButton(InputAction.CallbackContext context)
    {
    }

    public override void HoldButton(InputAction.CallbackContext context)
    {
    }
}
