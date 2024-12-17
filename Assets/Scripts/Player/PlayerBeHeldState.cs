using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// プレイヤー停止状態
/// </summary>
public class PlayerBeHeldState : PlayerStateMachine
{

    int cnt = 0;
    //コンストラクタ
    public PlayerBeHeldState()
    {

    }

    public override void Initialize(PlayerController player)
    {
        cnt = 0;
    }

    public override void Think(PlayerController player)
    {
        if (player.GetComponent<CharacterController>().enabled)
        {
            ++cnt;
            if (player.GetComponent<CharacterController>().isGrounded && cnt > 5)
            {
                player.ChangeState(player.PreState);
            }
        }
    }

    public override void Move(PlayerController player)
    {

    }

    /*β版まで未使用(InputSystem)
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
