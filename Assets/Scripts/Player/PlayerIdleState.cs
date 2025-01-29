using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
        //�N���A
        if (player.gameManager.isClear)
        {
            player.ChangeState(new PlayerClearState());
        }
        //���S
        if (player.gameManager.isGameOver)
        {
            player.ChangeState(new PlayerDeadState());
        }
    }

    public override void Move(PlayerController player)
    {
        //�d�͓͂�������
        player.UpdateMoveDirection(new Vector3(0f, player.GetMoveDirection().y, 0f));
    }
}
