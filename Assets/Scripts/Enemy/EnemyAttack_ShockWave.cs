using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_ShockWave : MonoBehaviour
{
    EnemyBase enemy;    // 敵の共通する値、処理を継承したスクリプトを保持する

    //衝撃波関係-------------------------------------------------------------------
    int         damage;                 // ダメージ量
    float       expandSpeed = 5f;       // 拡大スピード
    float       maxRadius = 10f;        // 最大半径
    int         arcPointsCount = 12;    // 円周上のポイント数
    GameObject  damagePointPrefab;      // ダメージ判定用プレハブ

    // Start is called before the first frame update
    void Start()
    {
        //敵の共通する値、処理を継承したスクリプトを取得する
        enemy = GetComponent<EnemyBase>();

        //値の設定
        if(enemy)
        {
            damage = enemy.attackPower;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
