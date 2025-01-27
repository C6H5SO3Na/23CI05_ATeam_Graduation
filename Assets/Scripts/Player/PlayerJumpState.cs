using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
/// <summary>
/// �v���C���[�W�����v���
/// </summary>
public class PlayerJumpState : PlayerStateMachine
{

    //�R���X�g���N�^
    public PlayerJumpState()
    {
        
    }

    public override void Initialize(PlayerController player)
    {
        //�W�����v����
        player.sound.PlayOneShot(player.SE.jumpSE);
        var jumpVec = new Vector3(player.GetInputDirection().x, 3f, player.GetInputDirection().z);
        player.UpdateMoveDirection(jumpVec);
    }

    public override void Think(PlayerController player)
    {
        //�n�ʂɒ��n������J��
        if (player.GetComponent<CharacterController>().isGrounded)
        {
            player.ChangeState(player.PreState);
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
        //�ړ�
        if (player.IsInputStick())
        {
            player.Walk();
        }
        else
        {
            player.Deceleration(0.9f);
        }
    }
}
