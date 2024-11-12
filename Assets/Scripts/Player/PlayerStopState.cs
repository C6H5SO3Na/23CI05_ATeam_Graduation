using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopState : PlayerStateMachine
{
    PlayerController player { get; set; }

    //コンストラクタ
    public PlayerStopState(in PlayerController player)
    {
        this.player = player;
    }

    void PlayerStateMachine.Initialize()
    {

    }

    void PlayerStateMachine.Think()
    {

    }

    void PlayerStateMachine.Move()
    {

    }
}
