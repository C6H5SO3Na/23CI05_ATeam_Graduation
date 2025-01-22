using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
/// <summary>
/// プレイヤーがものを投げる状態
/// </summary>
public class PlayerThrowState : PlayerStateMachine
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public PlayerThrowState()
    {

    }
    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="player">プレイヤー</param>
    public override void Initialize(PlayerController player)
    {

    }
    /// <summary>
    /// 思考
    /// </summary>
    /// <param name="player">プレイヤー</param>
    public override void Think(PlayerController player)
    {
        //アニメーションのために一瞬だけ
        player.ChangeState(player.PreState);
    }
    /// <summary>
    /// 行動
    /// </summary>
    /// <param name="player">プレイヤー</param>
    public override void Move(PlayerController player)
    {
        //すぐに終わるため空実装
    }
}
