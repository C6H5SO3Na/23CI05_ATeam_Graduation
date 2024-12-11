using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack_ShockWave : AttackBase
{
    //コンストラクタ----------------------------------------------------------
    public EnemyAttack_ShockWave()
    {
        boxSize = 4.0f;
    }

    //変数-------------------------------------------------------------------
    float       boxSize; // 四角のsize
    BoxCollider box;    // BoxColliderのインスタンス

    //関数-------------------------------------------------------------------
    //攻撃処理
    public override bool Attack ()
    {
        attackCount++;

        //攻撃予告オブジェクト生成(一回目の呼び出しでのみ行う)
        if (attackCount == 1)
        {
            attackNoticeObjectGeneraterInstance.shockWaveNoticeObjectGeneration(boxSize);
        }

        //攻撃予告を消し、ダメージ判定(3秒後に攻撃し、20f残る)
        if (attackCount == GameManager.gameFPS * 3)
        {
            //攻撃予告を消す
            attackNoticeObjectGeneraterInstance.DestroyAttackNoticeObject();

            //ダメージ判定を作る
            if(box == null)
            {
                box = attackOwner.AddComponent<BoxCollider>();
                box.isTrigger = true;
                box.size = new Vector3(boxSize / attackOwner.enemyTransform.localScale.x, boxSize / attackOwner.enemyTransform.localScale.y, boxSize / attackOwner.enemyTransform.localScale.z);
            }
        }
        //ダメージ判定を消す
        if (attackCount == GameManager.gameFPS * 3 + 20)
        {
            //ダメージ判定を消す
            if (box != null)
            {
                Object.Destroy(box);
            }

            //待機状態に遷移する
            return true;
        }

        return false;
    }
}