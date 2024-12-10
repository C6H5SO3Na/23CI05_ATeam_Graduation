using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    //ƒXƒe[ƒ^ƒXŠÖŒW-----------------------------------------------------------------
    //•Ï”
    public int healthPoint = 2;     // ‘Ì—Í
    public int attackPower = 1;     // UŒ‚—Í

    //s“®ŠÖŒW-----------------------------------------------------------------------
    //•Ï”
    public EnemyDied enemyDied { get; private set; }                        // “G€–S‚Ìs“®
    public Dictionary<string, IAttack> enemyAttacks { get; private set; }   // “GUŒ‚‚Ìs“®ƒŠƒXƒg
    public List<string> AttackTypes { get; private set; }                   // “G‚ªs‚¤UŒ‚s“®

    //ŠÖ”
    private void Awake()
    {
        //“G€–S‚Ìs“®İ’è
        enemyDied = new EnemyDied();

        //“GUŒ‚‚Ìs“®ƒŠƒXƒgİ’è
        enemyAttacks["ShockWave"] = new EnemyAttack_ShockWave();
        enemyAttacks["CreateDamageFloor"] = new EnemyAttack_CreateDamageFloor();
        enemyAttacks["ShowerBall"] = new EnemyAttack_ShowerBall();
    }
}