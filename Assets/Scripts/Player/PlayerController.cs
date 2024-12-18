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
    PlayerStateMachine state;//���
    public PlayerStateMachine State//�v���p�e�B
    {
        private set { state = value; }
        get { return state; }
    }

    PlayerStateMachine preState;//�O�̏��
    public PlayerStateMachine PreState//�v���p�e�B
    {
        private set { preState = value; }
        get { return preState; }
    }

    [SerializeField] float gravity;//�d��
    [SerializeField] float speed;//���x
    [SerializeField] int playerNum;//�v���C���[�ԍ�
    public int PlayerNum//�v���p�e�B
    {
        set
        {
            if (value <= 2) { playerNum = value; }
        }
        get { return playerNum; }
    }

    Vector3 moveDirection;//��������
    int invincibleCnt = 0;

    bool isInvincible;//���G����
    public bool IsInvincible//�v���p�e�B
    {
        private set { isInvincible = value; }
        get { return isInvincible; }
    }


    bool isHolding = false;//�����Ă��邩�̃t���O
    public bool IsHolding//�v���p�e�B
    {
        private set { isHolding = value; }
        get { return isHolding; }
    }

    string playerName;//InputManager�p
    public string PlayerName//�v���p�e�B
    {
        private set { playerName = value; }
        get { return playerName; }
    }

    const float debounceTime = 0.2f;//�f�o�E���X����
    float lastButtonPressTime = 0f;//�O�Ƀ{�^��������������

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

        //���ۂɓ���
        //Debug.Log($"moveDirection:{moveDirection} state:{state}");
        if (controller.enabled)
        {
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        //�d�͂𓭂�����
        if (!controller.isGrounded || state is PlayerBeHeldState)
        {
            WorkGravity(gravity);
        }

        //���G����
        if (isInvincible)
        {
            --invincibleCnt;
            Invincible();
            if (invincibleCnt <= 0)
            {
                isInvincible = false;
            }
        }

        //�v���C���[�`�F���W(�V���O���v���C�̂�)
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
            //������
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
    /// ��ԑJ��
    /// </summary>
    /// <param name="state">�ύX��̏��</param>
    public void ChangeState(PlayerStateMachine state)
    {
        Vector3 savePosition = transform.position;
        preState = this.state;
        this.state = state;
        this.state.Initialize(this);
        transform.position = savePosition;
    }

    /*���ł܂Ŗ��g�p(InputSystem)
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
        //���̂�����
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
    /// moveDirection��Setter
    /// </summary>
    /// <param name="direction">�ړ�����</param>
    public void UpdateMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    /// <summary>
    /// moveDirection��Getter
    /// </summary>
    /// <returns>moveDirection�̒l</returns>
    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

    /// <summary>
    /// �d�͂𓭂�����
    /// </summary>
    /// <param name="g">�d��</param>
    void WorkGravity(float g)
    {
        moveDirection.y -= g;
    }

    /// <summary>
    /// ���͂����
    /// </summary>
    /// <returns></returns>
    public Vector3 GetInputDirection()
    {
        return new Vector3(Input.GetAxis("Horizontal" + playerName), 0f, Input.GetAxis("Vertical" + playerName));
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void Walk()
    {
        Vector3 inputDirection = GetInputDirection();
        UpdateMoveDirection(new Vector3(inputDirection.x, moveDirection.y, inputDirection.z));

        //��]
        float normalizedDir = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, moveDirection.x + normalizedDir, 0.0f);
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Deceleration(float amount)
    {
        moveDirection.x *= amount;
        moveDirection.z *= amount;
    }

    /// <summary>
    /// �����鏈��
    /// </summary>
    public void Throw()
    {
        Transform parentTransform = transform;
        foreach (Transform child in parentTransform)
        {
            if (IsAbleHold(child))
            {
                //�e�q�֌W������
                child.gameObject.transform.SetParent(null);

                var angle = new Vector3(transform.forward.x, 0f, transform.forward.z);
                //�������Z�𕜊�������
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
    /// �u������
    /// </summary>
    public void Put()
    {
        Transform parentTransform = transform;
        foreach (Transform child in parentTransform)
        {
            if (IsAbleHold(child))
            {
                //�e�q�֌W������
                child.gameObject.transform.SetParent(null);

                var angle = new Vector3(transform.forward.x, 0f, transform.forward.z);
                //�������Z�𕜊�������
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
    /// ������
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
    /// ������
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
    /// ������
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
    /// ���X�e�B�b�N����
    /// </summary>
    /// <returns>���͂���Ă����true</returns>
    public bool IsInputStick()
    {
        return Input.GetAxis("Horizontal" + playerName) != 0f || Input.GetAxis("Vertical" + playerName) != 0f;
    }

    bool IsAbleThrow()
    {
        //�����Ă���&�����Ă����莞�Ԃ��o�߂���
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
    /// ���I�u�W�F�N�g�̃C���X�^���X���擾����
    /// </summary>
    /// <param name="gameManager"> GameManager�R���|�[�l���g </param>
    public void SetInstance(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}