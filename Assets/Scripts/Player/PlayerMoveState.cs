using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// �v���C���[���s���
/// </summary>
public class PlayerMoveState : PlayerStateMachine
{
    bool leaveMove = false;
    //bool isJump = false;//���g�p
    //�R���X�g���N�^
    public PlayerMoveState()
    {

    }

    public override void Initialize(PlayerController player)
    {

    }

    public override void Think(PlayerController player)
    {
        //�����Ă���Ƃ����ƃW�����v�ł��Ȃ�
        if (Input.GetButtonDown("Jump" + player.PlayerName) && !player.IsHolding)
        {
            player.ChangeState(new PlayerJumpState());
        }

        if (leaveMove)
        {
            player.ChangeState(new PlayerIdleState());
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
        //���͌�
        if (player.IsInputStick())
        {
            player.Walk();
        }
        else
        {
            leaveMove = true;
        }
    }
}
