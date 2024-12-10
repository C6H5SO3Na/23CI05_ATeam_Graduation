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

    //コンストラクタ
    public PlayerBeHeldState()
    {

    }

    public override void Initialize(PlayerController player)
    {

    }

    public override void Think(PlayerController player)
    {
        if (player.GetComponent<CharacterController>().enabled)
        {
            if (Physics.Raycast(player.transform.position, Vector3.down, out RaycastHit _, 0.1f)
                && !player.GetComponent<Rigidbody>().isKinematic)
            {
                player.ChangeState(player.PreState);
                player.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    public override void Move(PlayerController player)
    {
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
