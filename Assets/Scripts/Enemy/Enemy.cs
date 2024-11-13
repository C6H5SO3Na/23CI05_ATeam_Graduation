using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //ステータス関係-----------------------------------------------------------------
    //構造体
    /// <summary>
    /// 敵共通のステータス
    /// </summary>
    struct Status
    {
        int healthPoint;    // 体力
        int attackPower;    // 攻撃力
    }
}