using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
/// <summary>
/// �v���C���[�����̂𓊂�����
/// </summary>
public class PlayerThrowState : PlayerStateMachine
{
    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    public PlayerThrowState()
    {

    }
    /// <summary>
    /// ������
    /// </summary>
    /// <param name="player">�v���C���[</param>
    public override void Initialize(PlayerController player)
    {

    }
    /// <summary>
    /// �v�l
    /// </summary>
    /// <param name="player">�v���C���[</param>
    public override void Think(PlayerController player)
    {
        //�A�j���[�V�����̂��߂Ɉ�u����
        player.ChangeState(player.PreState);
    }
    /// <summary>
    /// �s��
    /// </summary>
    /// <param name="player">�v���C���[</param>
    public override void Move(PlayerController player)
    {
        //�����ɏI��邽�ߋ����
    }
}
