using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerStateMachine state;
    PlayerStateMachine preState;

    PlayerInput playerInput;
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

    public void Move()
    {

    }
}