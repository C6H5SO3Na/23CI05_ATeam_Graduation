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
    public EnemyDied enemyDied { get; private set; }                            // “G€–S‚Ìs“®
    public Dictionary<string, AttackBase> enemyAttacks { get; private set; }    // “GUŒ‚‚Ìs“®ƒŠƒXƒg
    public List<string> attackTypes { get; private set; }                       // “G‚ªs‚¤UŒ‚s“®

    //ŠÖ”
    void Awake()
    {
        //“G€–S‚Ìs“®İ’è
        enemyDied = new EnemyDied();

        //“GUŒ‚‚Ìs“®ƒŠƒXƒgİ’è
        enemyAttacks = new Dictionary<string, AttackBase>();
        enemyAttacks["ShockWave"] = new EnemyAttack_ShockWave();
        enemyAttacks["CreateDamageFloor"] = new EnemyAttack_CreateDamageFloor();
        enemyAttacks["ShowerBall"] = new EnemyAttack_ShowerBall();

        //“G‚ªs‚¤UŒ‚‚Ìí—Ş–¼‚ğŒˆ‚ß‚é
        attackTypes = new List<string>();
        attackTypes.Add("CreateDamageFloor");
        attackTypes.Add("ShowerBall");
    }
}