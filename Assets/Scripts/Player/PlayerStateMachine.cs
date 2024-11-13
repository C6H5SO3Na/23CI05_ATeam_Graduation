using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface PlayerStateMachine
{
    void Initialize();
    void Think();
    void Move();

    void HandleInput(InputAction.CallbackContext context);
}