using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour//, IPlayerInput
{
    CharacterController controller;

    PlayerStateMachine state;//状態
    public PlayerStateMachine State//プロパティ
    {
        private set { state = value; }
        get { return state; }
    }

    PlayerStateMachine preState;//前の状態
    public PlayerStateMachine PreState//プロパティ
    {
        private set { preState = value; }
        get { return preState; }
    }

    [SerializeField] float gravity;//重力
    [SerializeField] float speed;//速度
    [SerializeField] int playerNum;//プレイヤー番号

    Vector3 moveDirection;//動く向き

    bool isHolding = false;//持っているかのフラグ
    public bool IsHolding//プロパティ
    {
        private set { isHolding = value; }
        get { return isHolding; }
    }

    public string playerName;
    const float debounceTime = 0.2f;//デバウンス時間
    float lastButtonPressTime = 0f;//前にボタンを押した時間

    //public static int buttonCnt = 0;//ボタンを押した回数(現在未使用)

    void Start()
    {
        Application.targetFrameRate = 60;
        playerName = "_P" + playerNum.ToString();
        state = new PlayerIdleState();
        state.Initialize(this);
        controller = GetComponent<CharacterController>();
        isHolding = false;
    }

    void Update()
    {
        state.Think(this);
        state.Move(this);

        //実際に動く
        moveDirection = new Vector3(moveDirection.x * speed, moveDirection.y, moveDirection.z * speed);

        //速度を抑制
        moveDirection.x = Mathf.Clamp(moveDirection.x, -speed, speed);
        moveDirection.z = Mathf.Clamp(moveDirection.z, -speed, speed);

        //重力を働かせる
        if (!controller.isGrounded)
        {
            WorkGravity(gravity);
        }

        //Debug.Log($"moveDirection:{moveDirection} state:{state}");

        if (state is not PlayerBeHeldState)
        {
            controller.Move(moveDirection * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
                //投げる
        if (Input.GetButtonDown("Hold" + playerName) && IsAbleThrow())
        {
            Throw();
        }
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
        if (other.gameObject == this.gameObject)
        {
            return;
        }
        //ものを持つ
        if (Input.GetButtonDown("Hold" + playerName))
        {
            if (IsAbleHold(other))
            {
                if (other.transform.parent == null && !isHolding)
                {
                    Hold(other.gameObject);
                }
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
        return new Vector3(Input.GetAxis("Horizontal" + playerName), 0f, Input.GetAxis("Vertical" + playerName));
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
    /// 減速
    /// </summary>
    public void Deceleration(float amount)
    {
        moveDirection.x *= amount;
        moveDirection.z *= amount;
    }

    /// <summary>
    /// 投げる処理
    /// </summary>
    public void Throw()
    {
        Transform parentTransform = transform;
        foreach (Transform child in parentTransform)
        {
            if (IsAbleHold(child))
            {
                //親子関係を解除
                child.gameObject.transform.SetParent(null);

                //物理演算を復活させる
                child.GetComponent<Rigidbody>().isKinematic = false;

                if (child.GetComponent<CharacterController>() != null)
                {
                    child.GetComponent<CharacterController>().enabled = true;
                }
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
        switch (other.tag)
        {
            case "ThrowingObject":
                HoldObject(other);
                break;

            case "Player":
                HoldPlayer(other);
                break;
        }
    }

    /// <summary>
    /// 持つ処理
    /// </summary>
    public void HoldPlayer(GameObject other)
    {
        other.GetComponent<PlayerController>().UpdateMoveDirection(Vector3.zero);

        other.transform.localPosition = new Vector3(0f, 2f, 0f);
        other.GetComponent<PlayerController>().ChangeState(new PlayerBeHeldState());
        other.transform.rotation = Quaternion.identity;
        other.transform.SetParent(this.transform, false);
        other.GetComponent<CharacterController>().enabled = false;
        isHolding = true;
        lastButtonPressTime = Time.time;
    }

    /// <summary>
    /// 持つ処理
    /// </summary>
    public void HoldObject(GameObject other)
    {
        other.transform.rotation = Quaternion.identity;
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
        return Input.GetAxis("Horizontal" + playerName) != 0f || Input.GetAxis("Vertical" + playerName) != 0f;
    }

    bool IsAbleThrow()
    {
        //持っている&持ってから一定時間が経過した
        return isHolding && Time.time - lastButtonPressTime > debounceTime;
    }

    bool IsAbleHold(Component component)
    {
        return component.CompareTag("ThrowingObject") || component.CompareTag("Player");
    }
}