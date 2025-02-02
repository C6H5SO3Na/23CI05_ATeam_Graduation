using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour, IReduceHP
{
    CharacterController controller;
    PlayerUIManager ui;

    public AudioSource sound;
    public PlayerSE SE;
    public GameManager gameManager;
    PlayerAnimation playerAnimation;
    PlayerStateMachine state;//���

    public PlayerParticle particle;

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
    int originalPlayerNum;//�v���C���[�̏����ԍ�
    public int OriginalPlayerNum//�v���p�e�B
    {
        set
        {
            if (value <= 2) { originalPlayerNum = value; }
        }
        get { return originalPlayerNum; }
    }
    int nowPlayerNum; //���݂̃v���C���[�ԍ�
    public int NowPlayerNum//�v���p�e�B
    {
        set
        {
            if (value <= 2) { nowPlayerNum = value; }
        }
        get { return nowPlayerNum; }
    }
    Vector3 moveDirection;//��������
    Vector3 windMoveDirection;//���̓���
    public Vector3 WindMoveDirection
    {
        set { windMoveDirection = value; }
        get { return windMoveDirection; }
    }
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

    //�W�����v�̍���
    [SerializeField] float jumpPower;
    public float JumpPower
    {
        set { jumpPower = value; }
        get { return jumpPower; }
    }

    //���̋���
    public struct WindPower
    {
        //�󂯂����̋���
        float received;
        public float Received
        {
            set { received = value; }
            get { return received; }
        }

        //�󂯂Ă���ő�̕��̋���
        float max;
        public float Max
        {
            set { max = value; }
            get { return max; }
        }
    }

    public WindPower windPower;

    void Awake()
    {
        nowPlayerNum = originalPlayerNum;
    }

    void Start()
    {
        nowPlayerNum = originalPlayerNum;
        playerName = "_P" + nowPlayerNum.ToString();

        state = new PlayerIdleState();//�����X�e�[�g
        state.Initialize(this);

        controller = GetComponent<CharacterController>();

        sound = GetComponent<AudioSource>();
        SE = GetComponent<PlayerSE>();

        particle = GetComponent<PlayerParticle>();

        isHolding = false;
        gameManager.PlayersHP = 3;

        playerAnimation = transform.GetChild(0).GetChild(0).GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if (PauseScreenManager.IsPause) { return; }//�|�[�Y���͒�~
        state.Think(this);
        state.Move(this);

        //���ۂɓ���
        //Debug.Log($"moveDirection:{moveDirection} state:{state}");
        if (controller.enabled)
        {
            controller.Move(speed * Time.deltaTime * (moveDirection + WindMoveDirection));
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
                Renderer[] renderers = transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();
                foreach (Renderer renderer in renderers)
                {
                    renderer.material.SetFloat("_Surface", 0);
                    renderer.material.renderQueue = (int)RenderQueue.Geometry;
                    renderer.material.color = Color.white;
                    // URP�̐ݒ��ύX
                    renderer.material.SetOverrideTag("RenderType", "Opaque");
                    renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    renderer.material.SetInt("_ZWrite", 1);
                    renderer.material.DisableKeyword("_ALPHATEST_ON");
                    renderer.material.DisableKeyword("_ALPHABLEND_ON");
                    renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    renderer.material.renderQueue = (int)RenderQueue.Geometry;

                }
                isInvincible = false;
            }
        }

        //�v���C���[�`�F���W(�V���O���v���C�̂�)
        if (Input.GetButtonDown("Fire3") && !GameManager.isMultiPlay)
        {
            if (nowPlayerNum == 1)
            {
                nowPlayerNum = 2;
                ui.gameObject.SetActive(false);
            }
            else
            {
                nowPlayerNum = 1;
                ui.gameObject.SetActive(true);
            }
            playerName = "_P" + nowPlayerNum.ToString();
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
        playerAnimation.ChangeAnimation(state);
        transform.position = savePosition;
    }


    void OnTriggerStay(Collider other)
    {
        //���̂�����
        if (other.gameObject == this.gameObject) { return; }
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
        sound.PlayOneShot(SE.throwSE);
        ChangeState(new PlayerThrowState());
        Transform parentTransform = transform;
        foreach (Transform child in parentTransform)
        {
            if (IsAbleHold(child))
            {
                //�e�q�֌W������
                child.gameObject.transform.SetParent(null);

                var angle = new Vector3(transform.forward.x, 0.5f, transform.forward.z);
                //�������Z�𕜊�������
                if (child.GetComponent<Rigidbody>() != null)
                {
                    child.GetComponent<Rigidbody>().isKinematic = false;
                    child.GetComponent<Rigidbody>().AddForce(angle * 100f);
                }
                else if (child.GetComponent<CharacterController>() != null)
                {
                    child.GetComponent<CharacterController>().enabled = true;
                    child.GetComponent<PlayerController>().UpdateMoveDirection(angle * 4f);
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

                var angle = new Vector3(transform.forward.x, -1f, transform.forward.z);
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
        sound.PlayOneShot(SE.holdSE);
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

    /// <summary>
    /// ���G���Ԃ̏���
    /// </summary>
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

        Renderer[] renderers = transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.material.SetFloat("_Surface", 1);
            renderer.material.renderQueue = (int)RenderQueue.Transparent;
            renderer.material.color = newColor;

            // URP�̐ݒ��ύX
            renderer.material.SetOverrideTag("RenderType", "Transparent");
            renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            renderer.material.SetInt("_ZWrite", 0);
            renderer.material.EnableKeyword("_ALPHATEST_ON");
            renderer.material.EnableKeyword("_ALPHABLEND_ON");
            renderer.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            renderer.material.renderQueue = (int)RenderQueue.Transparent;
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

    /// <summary>
    /// �_���[�W���󂯂�
    /// </summary>
    /// <param name="damage">�_���[�W��</param>
    public void ReduceHP(int damage)
    {
        if (isInvincible || gameManager.PlayersHP <= 0) { return; }

        var position = new Vector3(0f, 1f, 0f);
        particle.PlayParticle(particle.damageParticle, position, transform);

        sound.PlayOneShot(SE.damageSE);
        isInvincible = true;
        invincibleCnt = 100;
        gameManager.ReceiveDamageInformation(damage);
    }
    /// <summary>
    /// UI���Z�b�g
    /// </summary>
    /// <param name="ui">UI</param>
    public void SetUI(PlayerUIManager ui)
    {
        this.ui = ui;
    }

}