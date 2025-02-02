using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UIElements;
/// <summary>
/// プレイヤージャンプ状態
/// </summary>
public class PlayerJumpState : PlayerStateMachine
{
    //着地用変数
    bool isLand = false;

    //地面を離れたか
    bool isLeft = false;
    //コンストラクタ
    public PlayerJumpState()
    {

    }

    public override void Initialize(PlayerController player)
    {
        //ジャンプする
        player.sound.PlayOneShot(player.SE.jumpSE);
        player.particle.PlayParticle(player.particle.jumpAndLandParticle, Vector3.zero, player.transform);
        //ジャンプと下からの風の強さ
        var jumpVec = new Vector3(player.GetInputDirection().x, player.JumpPower + player.windPower.Received, player.GetInputDirection().z);
        player.UpdateMoveDirection(jumpVec);

        isLand = false;
    }

    public override void Think(PlayerController player)
    {
        //地面に着地したら遷移
        if (isLand)
        {
            player.ChangeState(player.PreState);
        }
        //クリア
        if (player.gameManager.isClear)
        {
            player.ChangeState(new PlayerClearState());
        }
        //死亡
        if (player.gameManager.isGameOver)
        {
            player.ChangeState(new PlayerDeadState());
        }
    }

    public override void Move(PlayerController player)
    {
        //地面に着地したらエフェクトを出す
        if (player.GetComponent<CharacterController>().isGrounded)
        {
            //ジャンプ状態になった最初は着地した扱いになるため
            if (!isLeft)
            {
                isLeft = true;
            }
            //着地したときはこちら
            else
            {
                player.particle.PlayParticle(player.particle.jumpAndLandParticle, Vector3.zero, player.transform);
                isLand = true;
            }
        }
        //移動
        if (player.IsInputStick())
        {
            player.Walk();
        }
        else
        {
            player.Deceleration(0.9f);
        }
    }
}
