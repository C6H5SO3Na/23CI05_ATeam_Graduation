using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// プレイヤークリア状態
/// </summary>
public class PlayerClearState : PlayerStateMachine
{

    //コンストラクタ
    public PlayerClearState()
    {

    }

    public override void Initialize(PlayerController player)
    {
        player.UpdateMoveDirection(Vector3.zero);
    }

    public override void Think(PlayerController player)
    {

    }

    public override void Move(PlayerController player)
    {

    }
}
