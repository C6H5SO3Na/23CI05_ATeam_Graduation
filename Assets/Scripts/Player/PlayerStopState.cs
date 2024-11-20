using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStopState : PlayerStateMachine
{
    PlayerController player { get; set; }

    //コンストラクタ
    public PlayerStopState(in PlayerController player)
    {
        this.player = player;
    }

    void PlayerStateMachine.Initialize()
    {

    }

    void PlayerStateMachine.Think()
    {

    }

    void PlayerStateMachine.Move()
    {
        //Debug.Log("Stop");
    }

    void PlayerStateMachine.HandleInput(InputAction.CallbackContext context)
    {
        var axis = context.ReadValue<Vector2>();
        Debug.Log("入力");
    }
}
