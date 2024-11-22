using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IPlayerInput
{
    PlayerStateMachine state;
    public PlayerStateMachine preState;
    [SerializeField] float gravity;
    [SerializeField] float speed;
    CharacterController controller;
    Vector3 moveVec;
    public static int cnt = 0;
    //èâä˙âª
    void Start()
    {
        state = new PlayerIdleState(this);
        state.Initialize();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        state.Think();
        state.Move();
        moveVec = state.GetMoveVec();
        moveVec = new Vector3(moveVec.x * speed, moveVec.y, moveVec.z * speed);
        state.WorkGravity(gravity);
        controller.Move(moveVec * Time.deltaTime);
    }

    public void ChangeState(PlayerStateMachine state)
    {
        preState = this.state;
        this.state = state;
        this.state.Initialize();
    }

    public void MoveButton(InputAction.CallbackContext context)
    {
        //Debug.Log(++cnt);
        var axis = context.ReadValue<Vector2>();
        //Debug.Log(state);
        state.MoveButton(context);
    }

    public void JumpButton(InputAction.CallbackContext context)
    {
        state.JumpButton(context);
        Debug.Log(state);
    }

    public void HoldButton(InputAction.CallbackContext context)
    {
    }
}