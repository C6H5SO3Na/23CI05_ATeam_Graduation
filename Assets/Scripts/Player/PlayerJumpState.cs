using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UIElements;
/// <summary>
/// �v���C���[�W�����v���
/// </summary>
public class PlayerJumpState : PlayerStateMachine
{
    //���n�p�ϐ�
    bool isLand = false;

    //�n�ʂ𗣂ꂽ��
    bool isLeft = false;
    //�R���X�g���N�^
    public PlayerJumpState()
    {

    }

    public override void Initialize(PlayerController player)
    {
        //�W�����v����
        player.sound.PlayOneShot(player.SE.jumpSE);
        player.particle.PlayParticle(player.particle.jumpAndLandParticle, Vector3.zero, player.transform);
        //�W�����v�Ɖ�����̕��̋���
        var jumpVec = new Vector3(player.GetInputDirection().x, player.JumpPower + player.windPower.Received, player.GetInputDirection().z);
        player.UpdateMoveDirection(jumpVec);

        isLand = false;
    }

    public override void Think(PlayerController player)
    {
        //�n�ʂɒ��n������J��
        if (isLand)
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
        //�n�ʂɒ��n������G�t�F�N�g���o��
        if (player.GetComponent<CharacterController>().isGrounded)
        {
            //�W�����v��ԂɂȂ����ŏ��͒��n���������ɂȂ邽��
            if (!isLeft)
            {
                isLeft = true;
            }
            //���n�����Ƃ��͂�����
            else
            {
                player.particle.PlayParticle(player.particle.jumpAndLandParticle, Vector3.zero, player.transform);
                isLand = true;
            }
        }
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
