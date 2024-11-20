using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �X�e�[�g�}�V���̃C���^�[�t�F�C�X
/// </summary>
public interface IPlayerStateMachine
{
    /// <summary>
    /// ������
    /// </summary>
    void Initialize();
    /// <summary>
    /// �v�l
    /// </summary>
    void Think();
    /// <summary>
    /// �s��
    /// </summary>
    void Move();
}
