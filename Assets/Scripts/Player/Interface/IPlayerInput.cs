using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ���̓C���^�[�t�F�C�X
/// </summary>
public interface IPlayerInput
{
    /// <summary>
    /// �㉺���E�ړ�
    /// </summary>
    /// <param name="context">���̓f�[�^</param>
    void MoveButton(InputAction.CallbackContext context);

    /// <summary>
    /// �W�����v
    /// </summary>
    /// <param name="context">���̓f�[�^</param>
    void JumpButton(InputAction.CallbackContext context);

    /// <summary>
    /// ���E������
    /// </summary>
    /// <param name="context">���̓f�[�^</param>
    void HoldButton(InputAction.CallbackContext context);

}
