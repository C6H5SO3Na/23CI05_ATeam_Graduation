using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour//, IPlayerInput
{
    CharacterController controller;

    PlayerStateMachine state;//状態
    PlayerStateMachine preState;//前の状態
    public PlayerStateMachine PreState//プロパティ
    {
        private set { preState = value; }
        get { return preState; }
    }

    [SerializeField] float gravity;//重力
    [SerializeField] float speed;//速度

    Vector3 moveDirection;//動く向き

    bool isHolding = false;//持っているかのフラグ
    public bool IsHolding//プロパティ
    {
        private set { isHolding = value; }
        get { return isHolding; }
    }

    const float debounceTime = 0.2f;//デバウンス時間
    float lastButtonPressTime = 0f;//前にボタンを押した時間

    //public static int buttonCnt = 0;//ボタンを押した回数(現在未使用)

    //初期化
    void Start()
    {
        state = new PlayerIdleState();
        state.Initialize(this);
        controller = GetComponent<CharacterController>();
        isHolding = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state);
        state.Think(this);
        state.Move(this);

        //実際に動く
        moveDirection = new Vector3(moveDirection.x * speed, moveDirection.y, moveDirection.z * speed);

        //重力を働かせる
        if (!controller.isGrounded)
        {
            WorkGravity(gravity);
        }

        //投げる
        if (Input.GetButtonDown("Hold") && IsAbleThrow())
        {
            Throw();
        }
        controller.Move(moveDirection * Time.deltaTime);
    }

    /// <summary>
    /// 状態遷移
    /// </summary>
    /// <param name="state">変更後の状態</param>
    public void ChangeState(PlayerStateMachine state)
    {
        preState = this.state;
        this.state = state;
        this.state.Initialize(this);
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
        //ものを持つ
        if (other.CompareTag("ThrowingObject") && Input.GetButtonDown("Hold"))
        {
            if (other.transform.parent == null && !isHolding)
            {
                Hold(other.gameObject);
            }
        }
    }
    /// <summary>
    /// moveDirectionのSetter
    /// </summary>
    /// <param name="direction">移動方向</param>
    public void UpdateMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    /// <summary>
    /// moveDirectionのGetter
    /// </summary>
    /// <returns>moveDirectionの値</returns>
    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

    /// <summary>
    /// 重力を働かせる
    /// </summary>
    /// <param name="g">重力</param>
    void WorkGravity(float g)
    {
        moveDirection.y -= g;
    }

    /// <summary>
    /// 入力を入手
    /// </summary>
    /// <returns></returns>
    public Vector3 GetInputDirection()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
    }

    /// <summary>
    /// 歩く処理
    /// </summary>
    public void Walk()
    {
        Vector3 inputDirection = GetInputDirection();
        UpdateMoveDirection(new Vector3(inputDirection.x, moveDirection.y, inputDirection.z));

        //回転
        float normalizedDir = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, moveDirection.x + normalizedDir, 0.0f);
    }

    /// <summary>
    /// 投げる処理
    /// </summary>
    public void Throw()
    {
        Transform parentTransform = transform;
        foreach (Transform child in parentTransform)
        {
            if (child.CompareTag("ThrowingObject"))
            {
                //親子関係を解除
                child.gameObject.transform.SetParent(null);

                //物理演算を復活させる
                child.GetComponent<Rigidbody>().isKinematic = false;
                var angle = new Vector3(transform.forward.x, 0f, transform.forward.z);
                child.GetComponent<Rigidbody>().AddForce(angle * 100f);
                isHolding = false;
            }
        }
    }

    /// <summary>
    /// 持つ処理
    /// </summary>
    public void Hold(GameObject other)
    {
        other.transform.rotation = this.transform.rotation;
        other.transform.SetParent(this.transform, false);
        other.transform.localPosition = new Vector3(0f, 2f, 0f);
        other.GetComponent<Rigidbody>().isKinematic = true;
        isHolding = true;
        lastButtonPressTime = Time.time;
    }

    /// <summary>
    /// 左スティック入力
    /// </summary>
    /// <returns>入力されていればtrue</returns>
    public bool IsInputStick()
    {
        return Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f;
    }

    bool IsAbleThrow()
    {
        //持っている&持ってから一定時間が経過した
        return isHolding && Time.time - lastButtonPressTime > debounceTime;
    }
}