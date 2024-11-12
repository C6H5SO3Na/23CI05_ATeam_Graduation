using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerStateMachine
{
    PlayerController player { get; set; }

    //�R���X�g���N�^
    public PlayerMoveState(in PlayerController player)
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
        player.Move();
    }
}
