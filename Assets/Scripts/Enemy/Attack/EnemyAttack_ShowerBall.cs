using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_ShowerBall : AttackBase
{
    //攻撃処理
    public override bool Attack()
    {
        attackCount++;

        //降らせる玉の数を決め、生成する
        if(attackCount == 1)
        {

        }

        //1秒後に待機状態に戻る
        if(attackCount == GameManager.gameFPS)
        {
            return true;
        }

        return false;
    }
}
