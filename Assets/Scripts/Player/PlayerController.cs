using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour//, IPlayerInput
{
    CharacterController controller;

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

    Vector3 moveDirection;//��������

    bool isHolding = false;//�����Ă��邩�̃t���O
    public bool IsHolding//�v���p�e�B
    {
        private set { isHolding = value; }
        get { return isHolding; }
    }

    public string playerName;
    const float debounceTime = 0.2f;//�f�o�E���X����
    float lastButtonPressTime = 0f;//�O�Ƀ{�^��������������

    //public static int buttonCnt = 0;//�{�^������������(���ݖ��g�p)

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

        //���ۂɓ���
        moveDirection = new Vector3(moveDirection.x * speed, moveDirection.y, moveDirection.z * speed);

        //���x��}��
        moveDirection.x = Mathf.Clamp(moveDirection.x, -speed, speed);
        moveDirection.z = Mathf.Clamp(moveDirection.z, -speed, speed);

        //�d�͂𓭂�����
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
                //������
        if (Input.GetButtonDown("Hold" + playerName) && IsAbleThrow())
        {
            Throw();
        }
    }

    /// <summary>
    /// ��ԑJ��
    /// </summary>
    /// <param name="state">�ύX��̏��</param>
    public void ChangeState(PlayerStateMachine state)
    {
        preState = this.state;
        this.state = state;
        this.state.Initialize(this);
    }

    /*���łł͖��g�p(InputSystem)
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

                //�������Z�𕜊�������
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
}