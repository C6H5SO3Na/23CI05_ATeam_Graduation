using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// �v���C���[��~���
/// </summary>
public class PlayerBeHeldState : PlayerStateMachine
{

    int cnt = 0;
    //�R���X�g���N�^
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

    /*���ł܂Ŗ��g�p(InputSystem)
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
