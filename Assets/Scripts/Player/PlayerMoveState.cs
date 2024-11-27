using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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
        if (Input.GetButtonDown("Jump") && !player.IsHolding)
        {
            player.ChangeState(new PlayerJumpState());
        }

        if (leaveMove)
        {
            player.ChangeState(new PlayerIdleState());
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

    /*���łł͖��g�p(InputSystem)
    public override void MoveButton(InputAction.CallbackContext context)
    {
        var moveVec = context.ReadValue<Vector2>().normalized;
        velocity = new Vector3(moveVec.x, velocity.y, moveVec.y);
        Debug.Log(velocity);
        float normalizedDir = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0.0f, velocity.x + normalizedDir, 0.0f);
        if (!context.performed)
        {
            leaveMove = true;
        }
    }

    public override void JumpButton(InputAction.CallbackContext context)
    {
        //if (context.started) { isJump = true; }
    }

    public override void HoldButton(InputAction.CallbackContext context)
    {
    }
    */
}
