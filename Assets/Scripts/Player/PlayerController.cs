using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    PlayerStateMachine state;//状態

    public PlayerParticle particle;

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
    int originalPlayerNum;//プレイヤーの初期番号
    public int OriginalPlayerNum//プロパティ
    {
        set
        {
            if (value <= 2) { originalPlayerNum = value; }
        }
        get { return originalPlayerNum; }
    }
    int nowPlayerNum; //現在のプレイヤー番号
    public int NowPlayerNum//プロパティ
    {
        set
        {
            if (value <= 2) { nowPlayerNum = value; }
        }
        get { return nowPlayerNum; }
    }
    Vector3 moveDirection;//動く向き
    Vector3 windMoveDirection;//風の動き
    public Vector3 WindMoveDirection
    {
        set { windMoveDirection = value; }
        get { return windMoveDirection; }
    }
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

    //ジャンプの高さ
    [SerializeField] float jumpPower;
    public float JumpPower
    {
        set { jumpPower = value; }
        get { return jumpPower; }
    }

    //風の強さ
    public struct WindPower
    {
        //受けた風の強さ
        float received;
        public float Received
        {
            set { received = value; }
            get { return received; }
        }

        //受けている最大の風の強さ
        float max;
        public float Max
        {
            set { max = value; }
            get { return max; }
        }
    }

    public WindPower windPower;

    int animationCount = 0;//アニメーション用のカウント


    void Start()
    {
        nowPlayerNum = originalPlayerNum;
        playerName = "_P" + nowPlayerNum.ToString();

        state = new PlayerIdleState();//初期ステート
        state.Initialize(this);

        controller = GetComponent<CharacterController>();

        sound = GetComponent<AudioSource>();
        SE = GetComponent<PlayerSE>();

        particle = GetComponent<PlayerParticle>();

        isHolding = false;
        gameManager.PlayersHP = 3;

        playerAnimation = transform.GetChild(0).GetChild(0).GetComponent<PlayerAnimation>();
        animationCount = 0;
    }

    void Update()
    {
        if (PauseScreenManager.IsPause) { return; }//ポーズ中は停止
        state.Think(this);
        state.Move(this);

        //アニメーション速度更新
        playerAnimation.UpdateAnimationSpeed(state);

        //実際に動く
        //Debug.Log($"moveDirection:{moveDirection} state:{state}");
        if (controller.enabled)
        {
            controller.Move(speed * Time.deltaTime * (moveDirection + WindMoveDirection));
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
                Renderer[] renderers = transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();
                foreach (Renderer renderer in renderers)
                {
                    renderer.material.SetFloat("_Surface", 0);
                    renderer.material.renderQueue = (int)RenderQueue.Geometry;
                    renderer.material.color = Color.white;
                    // URPの設定を変更
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

        //プレイヤーチェンジ(シングルプレイのみ)
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
        playerAnimation.ChangeAnimation(state);
        transform.position = savePosition;
    }


    void OnTriggerStay(Collider other)
    {
        //ものを持つ
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
        //入力のベクトルの正規化
        if (inputDirection.magnitude > 1f)
        {
            inputDirection = inputDirection.normalized;
        }
        UpdateMoveDirection(new Vector3(inputDirection.x, moveDirection.y, inputDirection.z));

        //回転
        float normalizedDir = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, moveDirection.x + normalizedDir, 0.0f);

        //アニメーション
        ++animationCount;
        if (animationCount % (int)(15f / inputDirection.magnitude) == 0)//最大0.25秒に1回エフェクトを出す
        {
            particle.PlayParticle(particle.moveParticle, Vector3.zero, transform);
        }
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
        sound.PlayOneShot(SE.throwSE);
        ChangeState(new PlayerThrowState());
        Transform parentTransform = transform;
        foreach (Transform child in parentTransform)
        {
            if (IsAbleHold(child))
            {
                //親子関係を解除
                child.gameObject.transform.SetParent(null);

                var angle = new Vector3(transform.forward.x, 0.5f, transform.forward.z);
                //物理演算を復活させる
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

                var angle = new Vector3(transform.forward.x, -1f, transform.forward.z);
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

    /// <summary>
    /// 無敵時間の処理
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

            // URPの設定を変更
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
    /// 他オブジェクトのインスタンスを取得する
    /// </summary>
    /// <param name="gameManager"> GameManagerコンポーネント </param>
    public void SetInstance(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damage">ダメージ量</param>
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
    /// UIをセット
    /// </summary>
    /// <param name="ui">UI</param>
    public void SetUI(PlayerUIManager ui)
    {
        this.ui = ui;
    }

}