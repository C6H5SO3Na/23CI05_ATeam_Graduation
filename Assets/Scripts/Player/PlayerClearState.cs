using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// �v���C���[�N���A���
/// </summary>
public class PlayerClearState : PlayerStateMachine
{

    //�R���X�g���N�^
    public PlayerClearState()
    {

    }

    public override void Initialize(PlayerController player)
    {
        player.UpdateMoveDirection(Vector3.zero);
    }

    public override void Think(PlayerController player)
    {

    }

    public override void Move(PlayerController player)
    {

    }
}
