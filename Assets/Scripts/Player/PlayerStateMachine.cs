using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerStateMachine : IPlayerStateMachine//, IPlayerInput
{
    public abstract void Initialize(PlayerController player);
    public abstract void Think(PlayerController player);
    public abstract void Move(PlayerController player);
    /*ƒ¿”Å‚Å‚Í–¢Žg—p(InputSystem)
    public abstract void MoveButton(InputAction.CallbackContext context);
    public abstract void JumpButton(InputAction.CallbackContext context);
    public abstract void HoldButton(InputAction.CallbackContext context);
    */
}