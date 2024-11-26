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
    void Initialize();
    /// <summary>
    /// 思考
    /// </summary>
    void Think();
    /// <summary>
    /// 行動
    /// </summary>
    void Move();
}
