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
    //bool toMove = false;
    //bool isJump = false;

    //�R���X�g���N�^
    public PlayerIdleState()
    {
        //toMove = false;
        //isJump = false;
    }

    public override void Initialize(PlayerController player)
    {

    }

    public override void Think(PlayerController player)
    {
        if (Input.GetButtonDown("Jump" + player.PlayerName) && !player.IsHolding)
        {
            player.ChangeState(new PlayerJumpState());
        }
        if (player.IsInputStick())
        {
            player.ChangeState(new PlayerMoveState());
        }
    }

    public override void Move(PlayerController player)
    {
        //�d�͓͂�������
        player.UpdateMoveDirection(new Vector3(0f, player.GetMoveDirection().y, 0f));
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
