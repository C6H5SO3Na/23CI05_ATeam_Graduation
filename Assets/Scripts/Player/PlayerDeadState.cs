using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// �v���C���[���S���
/// </summary>
public class PlayerDeadState : PlayerStateMachine
{

    //�R���X�g���N�^
    public PlayerDeadState()
    {

    }

    public override void Initialize(PlayerController player)
    {
        player.sound.PlayOneShot(player.SE.deadSE);
        player.UpdateMoveDirection(Vector3.zero);
    }

    public override void Think(PlayerController player)
    {

    }

    public override void Move(PlayerController player)
    {

    }
}
