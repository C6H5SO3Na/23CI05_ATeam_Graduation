using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour//, IPlayerInput
{
    //���
    PlayerStateMachine state;

    //�O�̏��
    public PlayerStateMachine preState;

    //�d��
    [SerializeField] float gravity;

    //���x
    [SerializeField] float speed;

    //CharacterController
    CharacterController controller;

    //��������
    Vector3 moveVec;

    //�����Ă��邩�̃t���O
    bool doHold = false;

    //�f�o�E���X����
    float debounceTime = 0.2f;

    //�O�Ƀ{�^��������������
    float lastButtonPressTime = 0f;

    //�{�^������������(���ݖ��g�p)
    //public static int buttonCnt = 0;

    //������
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

        //���ۂɓ���
        moveVec = state.GetMoveVec();
        moveVec += new Vector3(moveVec.x * speed, moveVec.y, moveVec.z * speed);

        //�d�͂𓭂�����
        if (!controller.isGrounded)
        {
            state.WorkGravity(gravity);
        }

        //������
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
    /// ��ԑJ��
    /// </summary>
    /// <param name="state">�ύX��̏��</param>
    public void ChangeState(PlayerStateMachine state)
    {
        preState = this.state;
        this.state = state;
        this.state.Initialize();
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