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

    /*β版まで未使用(InputSystem)
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
    */
}
