using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour ,IPlayerInput
{
    PlayerStateMachine state;
    PlayerStateMachine preState;

    Rigidbody rb;
    //èâä˙âª
    void Start()
    {
        state = new PlayerStopState(this);
        state.Initialize();
        rb = GetComponent<Rigidbody>();
    }
    public void OnActionPerformed(InputAction.CallbackContext context)
    {
        state.HandleInput(context);
    }
    // Update is called once per frame
    void Update()
    {
        state.Think();
        state.Move();
    }

    public void ChangeState(PlayerStateMachine state)
    {
        preState = this.state;
        this.state = state;
        this.state.Initialize();
    }

    public void MoveButton(InputAction.CallbackContext context)
    {
        var axis = context.ReadValue<Vector2>();
        Debug.Log(axis);
    }

    public void JumpButton(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
    }

    public void HoldButton(InputAction.CallbackContext context)
    {
        Debug.Log("Hold");
    }

    public void Move(InputAction.CallbackContext context)
    {

    }
}