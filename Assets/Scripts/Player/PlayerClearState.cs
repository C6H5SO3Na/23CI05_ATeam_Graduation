using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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

    /*���ł܂Ŗ��g�p(InputSystem)
    public override void MoveButton(InputAction.CallbackContext context)
    {
        var moveVec = context.ReadValue<Vector2>();
        velocity = new Vector3(moveVec.x, velocity.y, moveVec.y);
        float normalizedDir = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0.0f, velocity.x + normalizedDir, 0.0f);
    }

    public override void JumpButton(InputAction.CallbackContext context)
    {
    }

    public override void HoldButton(InputAction.CallbackContext context)
    {
    }
    */
}
