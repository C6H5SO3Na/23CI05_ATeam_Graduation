using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    //ƒXƒe[ƒ^ƒXŠÖŒW-----------------------------------------------------------------
    //•Ï”
    public int healthPoint = 2;     // ‘Ì—Í
    public int attackPower = 1;     // UŒ‚—Í

    //s“®ŠÖŒW
    public EnemyDied enemyDied;     // “G€–S‚Ìs“®

    private void Awake()
    {
        enemyDied = new EnemyDied();
    }
}