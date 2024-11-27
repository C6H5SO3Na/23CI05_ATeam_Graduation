using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ステートマシンのインターフェイス
/// </summary>
public interface IPlayerStateMachine
{
    /// <summary>
    /// 初期化
    /// </summary>
    void Initialize(PlayerController player);
    /// <summary>
    /// 思考
    /// </summary>
    void Think(PlayerController player);
    /// <summary>
    /// 行動
    /// </summary>
    void Move(PlayerController player);
}
