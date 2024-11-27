using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour//, IPlayerInput
{
    //状態
    PlayerStateMachine state;

    //前の状態
    public PlayerStateMachine preState;

    //重力
    [SerializeField] float gravity;

    //速度
    [SerializeField] float speed;

    //CharacterController
    CharacterController controller;

    //動く向き
    Vector3 moveVec;

    //持っているかのフラグ
    bool doHold = false;

    //デバウンス時間
    float debounceTime = 0.2f;

    //前にボタンを押した時間
    float lastButtonPressTime = 0f;

    //ボタンを押した回数(現在未使用)
    //public static int buttonCnt = 0;

    //初期化
    void Start()
    {
        state = new PlayerIdleState(this);
        state.Initialize();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state);
        state.Think();
        state.Move();

        //実際に動く
        moveVec = state.GetMoveVec();
        moveVec += new Vector3(moveVec.x * speed, moveVec.y, moveVec.z * speed);

        //重力を働かせる
        if (!controller.isGrounded)
        {
            state.WorkGravity(gravity);
        }

        //投げる
        if (Input.GetButtonDown("Hold") && doHold && Time.time - lastButtonPressTime > debounceTime)
        {
            Transform parentTransform = transform;
            foreach (Transform child in parentTransform)
            {
                if (child.CompareTag("ThrowingObject"))
                {
                    child.gameObject.transform.SetParent(null);
                    child.GetComponent<Rigidbody>().isKinematic = false;
                    var angle = new Vector3(transform.forward.x, 0f, transform.forward.z);
                    child.GetComponent<Rigidbody>().AddForce(angle * 100f);
                    doHold = false;
                }
            }
        }
        Debug.Log(moveVec);
        controller.Move(moveVec * Time.deltaTime);
    }

    /// <summary>
    /// 状態遷移
    /// </summary>
    /// <param name="state">変更後の状態</param>
    public void ChangeState(PlayerStateMachine state)
    {
        preState = this.state;
        this.state = state;
        this.state.Initialize();
    }

    /*α版では未使用(InputSystem)
    public void MoveButton(InputAction.CallbackContext context)
    {
        Debug.Log(++buttonCnt);
        var axis = context.ReadValue<Vector2>();
        //Debug.Log(state);
        state.MoveButton(context);
    }

    public void JumpButton(InputAction.CallbackContext context)
    {
        state.JumpButton(context);
        //Debug.Log(state);
    }

    public void HoldButton(InputAction.CallbackContext context)
    {
    }
    */
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ThrowingObject") && Input.GetButtonDown("Hold"))
        {
            if (other.gameObject.transform.parent == null && !doHold)
            {
                other.gameObject.transform.rotation = this.transform.rotation;
                other.gameObject.transform.SetParent(this.transform, false);
                other.gameObject.transform.localPosition = new Vector3(0f, 2f, 0f);
                other.GetComponent<Rigidbody>().isKinematic = true;
                doHold = true;
                lastButtonPressTime = Time.time;
            }
        }
    }

}