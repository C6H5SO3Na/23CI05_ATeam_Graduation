using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 入力インターフェイス
/// </summary>
public interface IPlayerInput
{
    /// <summary>
    /// 上下左右移動
    /// </summary>
    /// <param name="context">入力データ</param>
    void MoveButton(InputAction.CallbackContext context);

    /// <summary>
    /// ジャンプ
    /// </summary>
    /// <param name="context">入力データ</param>
    void JumpButton(InputAction.CallbackContext context);

    /// <summary>
    /// 持つ・投げる
    /// </summary>
    /// <param name="context">入力データ</param>
    void HoldButton(InputAction.CallbackContext context);

}
