using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// プレイヤー死亡状態
/// </summary>
public class PlayerDeadState : PlayerStateMachine
{

    //コンストラクタ
    public PlayerDeadState()
    {

    }

    public override void Initialize(PlayerController player)
    {
        player.sound.PlayOneShot(player.SE.deadSE);
        player.UpdateMoveDirection(Vector3.zero);
    }

    public override void Think(PlayerController player)
    {

    }

    public override void Move(PlayerController player)
    {

    }
}
