using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : PlayerStateMachine
{
    PlayerController player { get; set; }
    Vector3 velocity;
    //コンストラクタ
    public PlayerMoveState(in PlayerController player)
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
        //Debug.Log("Move");
        player.transform.position += velocity * Time.deltaTime;
    }

    void PlayerStateMachine.HandleInput(InputAction.CallbackContext context)
    {
        var axis = context.ReadValue<Vector2>();
        velocity = new Vector3(axis.x, 0, axis.y);
    }
}
