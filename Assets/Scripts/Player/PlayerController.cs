using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour//, IPlayerInput
{
    CharacterController controller;
    PlayerUIManager ui;
    GameManager gameManager;
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
    public int PlayerNum//プロパティ
    {
        set
        {
            if (value <= 2) { playerNum = value; }
        }
        get { return playerNum; }
    }

    Vector3 moveDirection;//動く向き
    int invincibleCnt = 0;

    bool isInvincible;//無敵中か
    public bool IsInvincible//プロパティ
    {
        private set { isInvincible = value; }
        get { return isInvincible; }
    }


    bool isHolding = false;//持っているかのフラグ
    public bool IsHolding//プロパティ
    {
        private set { isHolding = value; }
        get { return isHolding; }
    }

    string playerName;//InputManager用
    public string PlayerName//プロパティ
    {
        private set { playerName = value; }
        get { return playerName; }
    }

    const float debounceTime = 0.2f;//デバウンス時間
    float lastButtonPressTime = 0f;//前にボタンを押した時間

    void Start()
    {
        playerName = "_P" + playerNum.ToString();
        state = new PlayerIdleState();
        state.Initialize(this);
        controller = GetComponent<CharacterController>();
        ui = transform.GetChild(1).GetComponent<PlayerUIManager>();
        isHolding = false;
        gameManager.PlayersHP = 3;
    }

    void Update()
    {
        state.Think(this);
        state.Move(this);

        //実際に動く
        //Debug.Log($"moveDirection:{moveDirection} state:{state}");
        if (controller.enabled)
        {
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        //重力を働かせる
        if (!controller.isGrounded || state is PlayerBeHeldState)
        {
            WorkGravity(gravity);
        }

        //無敵時間
        if (isInvincible)
        {
            --invincibleCnt;
            Invincible();
            if (invincibleCnt <= 0)
            {
                isInvincible = false;
            }
        }

        //プレイヤーチェンジ(シングルプレイのみ)
        if (Input.GetButtonDown("Fire3") && !GameManager.isMultiPlay)
        {
            if (playerNum == 1)
            {
                playerNum = 2;
                ui.gameObject.SetActive(false);
            }
            else
            {
                playerNum = 1;
                ui.gameObject.SetActive(true);
            }
            playerName = "_P" + playerNum.ToString();
        }
    }

    void FixedUpdate()
    {
        if (IsAbleThrow())
        {
            //投げる
            if (Input.GetButtonDown("Hold" + playerName))
            {
                Throw();
            }
            else if (Input.GetButtonDown("Put" + playerName))
            {
                Put();
            }
        }

    }

    /// <summary>
    /// 状態遷移
    /// </summary>
    /// <param name="state">変更後の状態</param>
    public void ChangeState(PlayerStateMachine state)
    {
        Vector3 savePosition = transform.position;
        preState = this.state;
        this.state = state;
        this.state.Initialize(this);
        transform.position = savePosition;
    }

    /*β版まで未使用(InputSystem)
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

    //
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            isInvincible = true;
            invincibleCnt = 100;
            gameManager.ReceiveDamageInformation();
        }
    }

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

                var angle = new Vector3(transform.forward.x, 0f, transform.forward.z);
                //物理演算を復活させる
                if (child.GetComponent<Rigidbody>() != null)
                {
                    child.GetComponent<Rigidbody>().isKinematic = false;
                    child.GetComponent<Rigidbody>().AddForce(angle * 100f);
                }
                else if (child.GetComponent<CharacterController>() != null)
                {
                    child.GetComponent<CharacterController>().enabled = true;
                    child.GetComponent<PlayerController>().UpdateMoveDirection(angle * 5f);
                }
                isHolding = false;
            }
        }
    }

    /// <summary>
    /// 置く処理
    /// </summary>
    public void Put()
    {
        Transform parentTransform = transform;
        foreach (Transform child in parentTransform)
        {
            if (IsAbleHold(child))
            {
                //親子関係を解除
                child.gameObject.transform.SetParent(null);

                var angle = new Vector3(transform.forward.x, 0f, transform.forward.z);
                //物理演算を復活させる
                if (child.GetComponent<Rigidbody>() != null)
                {
                    child.GetComponent<Rigidbody>().isKinematic = false;
                    child.transform.Translate(angle, Space.World);
                }
                else if (child.GetComponent<CharacterController>() != null)
                {
                    child.GetComponent<CharacterController>().enabled = true;
                    child.transform.Translate(angle, Space.World);
                }
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

    void Invincible()
    {
        Color newColor;
        if (invincibleCnt % 10 / 2 == 0)
        {
            newColor = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            newColor = new Color(1f, 1f, 1f, 1f);
        }

        Renderer[] renderers = transform.GetChild(1).GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = newColor;
        }
    }

    /// <summary>
    /// 他オブジェクトのインスタンスを取得する
    /// </summary>
    /// <param name="gameManager"> GameManagerコンポーネント </param>
    public void SetInstance(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}