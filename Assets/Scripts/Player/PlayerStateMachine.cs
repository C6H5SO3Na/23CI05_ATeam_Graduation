using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerStateMachine : IPlayerStateMachine, IPlayerInput
{
    /// <summary>
    /// プレイヤー本体
    /// </summary>
    protected PlayerController player;
    protected Vector3 velocity;

    public abstract void Initialize();
    public abstract void Think();
    public abstract void Move();
    public abstract void MoveButton(InputAction.CallbackContext context);
    public abstract void JumpButton(InputAction.CallbackContext context);
    public abstract void HoldButton(InputAction.CallbackContext context);
    public Vector3 GetMoveVec() { return velocity; }
    public void WorkGravity(float g) { velocity.y -= g; }
}