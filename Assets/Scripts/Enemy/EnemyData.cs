using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    //ステータス関係-----------------------------------------------------------------
    //変数
    public int healthPoint = 2;     // 体力
    public int attackPower = 1;     // 攻撃力

    //行動関係-----------------------------------------------------------------------
    //変数
    public EnemyDied enemyDied { get; private set; }                            // 敵死亡時の行動
    public Dictionary<string, AttackBase> enemyAttacks { get; private set; }    // 敵攻撃の行動リスト
    public List<string> attackTypes { get; private set; }                       // 敵が行う攻撃行動

    //関数
    void Awake()
    {
        //敵死亡時の行動設定
        enemyDied = new EnemyDied();

        //敵攻撃の行動リスト設定
        enemyAttacks = new Dictionary<string, AttackBase>();
        enemyAttacks["ShockWave"] = new EnemyAttack_ShockWave();
        enemyAttacks["CreateDamageFloor"] = new EnemyAttack_CreateDamageFloor();
        enemyAttacks["ShowerBall"] = new EnemyAttack_ShowerBall();

        //敵が行う攻撃の種類名を決める
        attackTypes = new List<string>();
        attackTypes.Add("CreateDamageFloor");
        attackTypes.Add("ShowerBall");
    }
}