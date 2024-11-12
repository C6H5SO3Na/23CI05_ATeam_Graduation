using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerStateMachine state;
    PlayerStateMachine preState;

    //èâä˙âª
    void Start()
    {
        state = new PlayerStopState(this);
        state.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        state.Think();
        state.Move();
    }

    void ChangeState(in PlayerStateMachine state)
    {
        preState = this.state;
        this.state = state;
        this.state.Initialize();
    }

    public void Move()
    {

    }
}