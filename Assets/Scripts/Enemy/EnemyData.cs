using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    //ステータス関係-----------------------------------------------------------------
    //変数
    public int healthPoint = 2;    // 体力
    public int attackPower = 1;    // 攻撃力

    //行動関係
    public EnemyDied enemyDied = new EnemyDied();


}